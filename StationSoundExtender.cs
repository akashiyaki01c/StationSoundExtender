using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AtsEx.PluginHost;
using AtsEx.PluginHost.Plugins;
using AtsEx.PluginHost.Native;
using BveTypes.ClassWrappers;

[Plugin(PluginType.MapPlugin)]
public class StationSoundExtender : AssemblyPluginBase
{
    private Scenario scenario = null;
    private StationSettings[] Settings = null;
    private StationSettings nowSetting = null;

    public StationSoundExtender(PluginBuilder builder) : base(builder) {
        this.BveHacker.ScenarioCreated += (ScenarioCreatedEventArgs e) => { 
            scenario = e.Scenario;
            InitSound();
            if (!scenario.Vehicle.Conductor.Doors.AreAllClosed)
            {
                // 始発駅かつ停車駅の場合に呼び出す
                OnDoorOpened(new DoorEventArgs());
            }
        };
        Native.DoorOpened += OnDoorOpened;
        Native.DoorClosed += OnDoorClosed;
        Settings = SettingsLoader.GetSettings();
    }

    public override TickResult Tick(TimeSpan elapsed)
    {
        if (nowSetting is null)
            return new MapPluginTickResult();
        if (scenario is null)
            return new MapPluginTickResult();

        var nowTime = scenario.TimeManager.Time;
        if (nowSetting.ArrivalSoundStopTime <= nowTime)
        {
            // 放送を止める処理
            GetSoundById(nowSetting.ArrivalSoundId)?.Stop(0);
        }
        if (!nowSetting.IsDepartureSoundPlayed && nowSetting.DepartureSoundTime <= nowTime)
        {
            // 出発放送を流す処理
            GetSoundById(nowSetting.DepartureSoundId)?.Play(1, 1, 0);
            nowSetting.IsDepartureSoundPlayed = true;
        }
        return new MapPluginTickResult();
    }

    public override void Dispose() {
        Native.DoorOpened -= OnDoorOpened;
        Native.DoorClosed -= OnDoorClosed;
    }

    /// <summary>
    /// 開扉時に呼び出される関数
    /// </summary>
    private void OnDoorOpened(DoorEventArgs e)
    {
        if (scenario is null) return;
        var next = GetNextStation();
        if (GetStationSettingsByName(next.Name) is null)
            return;
        nowSetting = GetStationSettingsByName(next.Name);
        nowSetting.IsDepartureSoundPlayed = false;
        var nowTime = scenario.TimeManager.Time;
        if (nowSetting.ArrivalSoundLimitTime >= nowTime)
        {
            // 放送を流す処理
            GetSoundById(nowSetting.ArrivalSoundId)?.Play(1, 1, 0);
        }
    }
    private void OnDoorClosed(DoorEventArgs e)
    {
        nowSetting = null;
    }

    /// <summary>
    /// 次駅を返す関数
    /// </summary>
    private Station GetNextStation()
    {
        var stations = scenario?.Route.Stations;
        var nextStaIndex = stations.CurrentIndex + 1;
        return stations.Count <= nextStaIndex ? null : stations[nextStaIndex] as Station;
    }
    /// <summary>
    /// 指定キーのサウンドを返す関数
    /// </summary>
    private Sound GetSoundById(string id)
    {
        if (id is null) return null;
        if (scenario is null) return null;
        scenario.Route.Sounds.TryGetValue(id, out var result);
        return result;
    }
    private Station[] GetStationArray()
    {
        if (scenario is null) return Array.Empty<Station>();
        var list = new List<Station>(scenario.Route.Stations.Count);
        for (var i = 0; i < scenario?.Route.Stations.Count; i++)
        {
            list.Add(scenario?.Route.Stations[i] as Station);
        }
        return list.ToArray();
    }
    private Station GetStationByName(string name)
    {
        if (name is null) return null;
        return GetStationArray().FirstOrDefault(v => {
            return (v as Station).Name == name;
        }) as Station;
    }
    private StationSettings GetStationSettingsByName(string name)
    {
        if (name is null) return null;
        return Settings.FirstOrDefault(v => {
            return v.TargetStationName == name;
        });
    }

    private void InitSound()
    {
        for (var i = 0; i < scenario?.Route.Stations.Count; i++)
        {
            var sta = scenario?.Route.Stations[i] as Station;
            if (sta is null) continue;
            if (!(GetStationSettingsByName(sta.Name) is null))
            {
                sta.ArrivalSound?.SetVolumeAndPitch(0, 0);
                sta.DepartureSound?.SetVolumeAndPitch(0, 0);
            }
        }
    }
}
