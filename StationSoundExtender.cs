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
        };
        Native.DoorOpened += OnDoorOpened;
        Native.DoorClosed += OnDoorClosed;
        Settings = SettingsLoader.GetSettings();
    }

    public override TickResult Tick(TimeSpan elapsed)
    {
        if (nowSetting is null)
        {
            return new MapPluginTickResult();
        }
        var nowTime = BveHacker.Scenario.TimeManager.Time;
        if (nowSetting.ArrivalSoundStopTime <= nowTime)
        {
            // 放送を止める処理
            GetSoundById(nowSetting.ArrivalSoundId).Stop(0);
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
        var nowTime = BveHacker.Scenario.TimeManager.Time;
        if (nowSetting.ArrivalSoundLimitTime >= nowTime)
        {
            // 放送を流す処理
            GetSoundById(nowSetting.ArrivalSoundId).Play(1, 1, 0);
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
        return this.BveHacker.Scenario.Route.Sounds[id];
    }
    private Station[] GetStationArray()
    {
        var list = new List<Station>(scenario.Route.Stations.Count);
        for (var i = 0; i < scenario?.Route.Stations.Count; i++)
        {
            list.Add(scenario?.Route.Stations[i] as Station);
        }
        return list.ToArray();
    }
    private Station GetStationByName(string name)
        => GetStationArray().FirstOrDefault(v => {
            return (v as Station).Name == name;
        }) as Station;
    private StationSettings GetStationSettingsByName(string name)
        => Settings.FirstOrDefault(v => {
            return v.TargetStationName == name;
        });

    private void InitSound()
    {
        for (var i = 0; i < scenario?.Route.Stations.Count; i++)
        {
            var sta = scenario?.Route.Stations[i] as Station;
            if (sta is null) continue;
            if (!(GetStationSettingsByName(sta.Name) is null))
            {
                sta.ArrivalSound?.SetVolumeAndPitch(0, 0);
            }
        }
    }
}
