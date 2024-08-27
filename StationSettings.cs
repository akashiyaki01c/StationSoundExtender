using System;
using System.Xml.Serialization;
using System.ComponentModel;

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
    [XmlIgnore]
    [Browsable(false)]
    public TimeSpan ArrivalSoundLimitTime { get; set; }
    [XmlElement("ArrivalSoundLimitTime")]
    public string ArrivalSoundLimitTimeString
    {
        get => ArrivalSoundLimitTime.ToString("hh:mm:ss");
        set => ArrivalSoundLimitTime = TimeSpan.Parse(value);
    }
    /// <summary>
    /// 到着放送を停止する時刻
    /// </summary>
    [XmlIgnore]
    [Browsable(false)]
    public TimeSpan ArrivalSoundStopTime { get; set; }
    [XmlElement("ArrivalSoundStopTime")]
    public string ArrivalSoundStopTimeString
    {
        get => ArrivalSoundStopTime.ToString("hh:mm:ss");
        set => ArrivalSoundStopTime = TimeSpan.Parse(value);
    }

    private string _departureSoundId;
    /// <summary>
    /// 出発時に放送するサウンド
    /// </summary>
    public string DepartureSoundId
    {
        get => _departureSoundId;
        set => _departureSoundId = value.ToLower();
    }
    /// <summary>
    /// 出発放送を再生する時刻
    /// </summary>
    [XmlIgnore]
    [Browsable(false)]
    public TimeSpan DepartureSoundTime { get; set; }
    [XmlElement("DepartureSoundTime")]
    public string DepartureSoundTimeString {
        get => DepartureSoundTime.ToString("hh:mm:ss");
        set => DepartureSoundTime = TimeSpan.Parse(value);
    }
    internal bool IsDepartureSoundPlayed { get; set; }
}