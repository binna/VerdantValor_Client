using System;
using System.IO;
using UnityEngine;

namespace Knight
{
    public static class JsonUtil
    {
        private static string GetPath(string fileName)
            => Path.Combine(Application.dataPath, $"GameData/{fileName}");

        public static void Save<T>(string fileName, T data, bool pretty = true)
        {
            try
            {
                var path = GetPath(fileName);
                var json = JsonUtility.ToJson(data, pretty);

                File.WriteAllText(path, json);

                if (File.Exists(path))
                    File.Replace(path, path, null);
                else
                    File.Move(path, path);
            }
            catch (Exception e)
            {
                Debug.LogError($"Json Save error: {e}");
            }
        }

        public static void Load<T>(string fileName, out T data)
        {
            try
            {
                var path = GetPath(fileName);

                if (!File.Exists(path))
                {
                    data = default;
                    return;
                }

                var json = File.ReadAllText(path);
                data = JsonUtility.FromJson<T>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Json Load error: {e}");
                data = default;
            }
        }
    }
}