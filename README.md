# StationSoundExtender
BVE上の駅放送を拡張するプラグインです。

## 機能

* 到着時に流れるサウンドを、延着時に流さないようにする。
* 指定の時間を過ぎると到着放送を中断する。

## 使い方
`StationSoundExtender.dll` と同じディレクトリ内に設定項目を記述した `StationSoundExtender.json` を配置します。

### 設定項目
```json
[
	{
		"TargetStationName": "A駅",             // 設定対象の駅名を記述します。(stationKeyではなく、stationNameで記述します。)
		"ArrivalSoundId": "StaK09_Arr",         // 到着時に再生するサウンド名を記述します。
		"ArrivalSoundLimitTime": "18:01:30",    // 到着放送を再生するリミットの時刻を記述します。
		"ArrivalSoundStopTime": "18:01:32"      // 到着放送の再生を中断する時刻を記述します。
	},
	{
		"TargetStationName": "B駅",             // 上記と同様の記述をします。
		"ArrivalSoundId": "StaK08_Arr",
		"ArrivalSoundLimitTime": "18:03:15",
		"ArrivalSoundStopTime": "18:03:17"
	}
]
```

## 使用ライブラリ
### [AtsEx.*](https://github.com/automatic9045/AtsEX) (MIT)
Copyright (c) 2022 automatic9045
