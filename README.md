# StationSoundExtender
AtsEX上でBVE上の駅放送を拡張するプラグインです。

## 機能

* 到着時に流れるサウンドを、延着時に流さないようにする。
* 指定の時間を過ぎると到着放送を中断する。
* 発車放送のタイミングを設定できる。

## 使い方
[リリースページ](https://github.com/akashiyaki01c/StationSoundExtender/releases) から `StationSoundExtender.dll` をダウンロードします。

シナリオ内に配置した`StationSoundExtender.dll` と同じディレクトリ内に設定項目を記述した `StationSoundExtender.xml` を配置します。

### 設定項目
```xml
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfStationSettings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
	<StationSettings>
		<TargetStationName>A駅</TargetStationName>                  // 設定対象の駅名を記述します。(stationKeyではなく、stationNameで記述します。)
		<ArrivalSoundId>StaArrS14</ArrivalSoundId>                  // 到着時に再生するサウンド名を記述します。
		<ArrivalSoundLimitTime>12:14:46</ArrivalSoundLimitTime>     // 到着放送を再生するリミットの時刻を記述します。
		<ArrivalSoundStopTime>12:14:55</ArrivalSoundStopTime>       // 到着放送の再生を中断する時刻を記述します。
		<DepartureSoundId>StaDep1</DepartureSoundId>                // 発車時に再生するサウンド名を記述します。
		<DepartureSoundTime>12:14:55</DepartureSoundTime>           // 発車放送を再生する時刻を記述します。
	</StationSettings>
	<StationSettings>                                               // 設定したい駅分だけ記述します。
		<TargetStationName>B駅</TargetStationName>
		<ArrivalSoundId>StaArrS12</ArrivalSoundId>
		<ArrivalSoundLimitTime>12:19:17</ArrivalSoundLimitTime>
		<ArrivalSoundStopTime>12:19:25</ArrivalSoundStopTime>
		<DepartureSoundId>StaDep1</DepartureSoundId>
		<DepartureSoundTime>12:19:25</DepartureSoundTime>
	</StationSettings>
</ArrayOfStationSettings>
```

## 使用ライブラリ
### [AtsEx.*](https://github.com/automatic9045/AtsEX) (MIT)
Copyright (c) 2022 automatic9045

### [System.Text.Json](https://www.nuget.org/packages/System.Text.Json) (MIT)
Copyright (c) .NET Foundation and Contributors