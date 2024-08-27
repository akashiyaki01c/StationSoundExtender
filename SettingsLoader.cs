using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

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
            var reader = new StreamReader(path);
            var serializer = new XmlSerializer(typeof(StationSettings[]));
            return (StationSettings[])serializer.Deserialize(reader);
        }
        catch
        {
            throw;
        }
    }

    private static string GetPath()
    {
        var asmPath = Assembly.GetExecutingAssembly().Location;
        var asmDir = Path.GetDirectoryName(asmPath);
        return asmDir + "/StationSoundExtender.xml";
    }
}
