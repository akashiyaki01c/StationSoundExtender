using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

/// <summary>
/// 設定ファイルの読み込みを行うクラス群
/// </summary>
public static class SettingsLoader
{
    public static StationSettings[] GetSettings()
    {
        try
        {
            var path = GetPath();
            var str = File.ReadAllBytes(path);
            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
            };
            return JsonSerializer.Deserialize<StationSettings[]>(str);
        }
        catch
        {
            return Array.Empty<StationSettings>();
        }
    }

    private static string GetPath()
    {
        var asmPath = Assembly.GetExecutingAssembly().Location;
        var asmDir = Path.GetDirectoryName(asmPath);
        return asmDir + "/StationSoundExtender.json";
    }
}
