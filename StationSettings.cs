using System;
using System.Collections.Generic;

public class StationSettings
{
    /// <summary>
    /// 設定対象の駅
    /// </summary>
    public string TargetStationName { get; set; }
    private string _arrivalSoundId;
    /// <summary>
    /// 到着時に放送するサウンド
    /// </summary>
    public string ArrivalSoundId {
        get => _arrivalSoundId;
        set => _arrivalSoundId = value.ToLower();
    }
    /// <summary>
    /// 到着放送を再生するリミット時刻
    /// </summary>
    public TimeSpan ArrivalSoundLimitTime { get; set; }
    /// <summary>
    /// 到着放送を停止する時刻
    /// </summary>
    public TimeSpan ArrivalSoundStopTime { get; set; }
}

internal static class TestSettings
{
    public static List<StationSettings> Settings => new List<StationSettings>() {
        new StationSettings() { 
            TargetStationName = "駒ヶ林", 
            ArrivalSoundId = "StaK09_Arr".ToLower(), 
            ArrivalSoundLimitTime = new TimeSpan(18, 1, 30) }
    };
}