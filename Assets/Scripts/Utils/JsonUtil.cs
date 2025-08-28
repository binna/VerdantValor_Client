using System;
using System.IO;
using UnityEngine;

namespace Knight
{
    public static class JsonUtil
    {
        public static void Save<T>(string fileName, T data)
        {
            try
            {
                var path = GetPath(fileName);
                var json = JsonUtility.ToJson(data, true);

                File.WriteAllText(path, json);
                
                Debug.Log($"Save Complete({fileName})");
            }
            catch (Exception e)
            {
                Debug.LogError($"Json Save error({fileName})\n{e}");
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
                Debug.LogError($"Json Load error({fileName})\n{e}");
                data = default;
            }
        }
        
        private static string GetPath(string fileName)
            => Path.Combine(Application.streamingAssetsPath, $"GameData/{fileName}");
    }
}