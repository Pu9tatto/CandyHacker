using System.Runtime.InteropServices;
using UnityEngine;

namespace AnotherArt
{
    /// <summary>
    /// UnityEngine.PlayerPrefs wrapper for WebGL LocalStorage
    /// </summary>
    public static class PlayerPrefs
    {
        public static void DeleteKey(string key)
        {
            //Debug.Log(string.Format("AnotherArt.DeleteKey(key: {0})", key));
            #if UNITY_WEBGL && !UNITY_EDITOR
            RemoveFromLocalStorage(key);
            #else
            UnityEngine.PlayerPrefs.DeleteKey(key);
            #endif
        }

        public static bool HasKey(string key)
        {
            //Debug.Log(string.Format("AnotherArt.HasKey(key: {0})", key));
            #if UNITY_WEBGL && !UNITY_EDITOR
            return (HasKeyInLocalStorage(key) == 1);
            #else
            return (UnityEngine.PlayerPrefs.HasKey(key));
            #endif
        }

        public static string GetString(string key)
        {
            //Debug.Log(string.Format("AnotherArt.GetString(key: {0})", key));
            #if UNITY_WEBGL && !UNITY_EDITOR
            return LoadFromLocalStorage(key);
            #else
            return (UnityEngine.PlayerPrefs.GetString(key));
            #endif
        }

        public static void SetString(string key, string value)
        {
            //Debug.Log(string.Format("AnotherArt.SetString(key: {0}, value: {1})", key, value));
            #if UNITY_WEBGL && !UNITY_EDITOR
            SaveToLocalStorage(key, value);
            #else
            UnityEngine.PlayerPrefs.SetString(key, value);
            #endif
        }

        public static void Save()
        {
            //Debug.Log(string.Format("AnotherArt.Save()"));
            #if !UNITY_WEBGL
            UnityEngine.PlayerPrefs.Save();
            #endif
        }

        #if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void SaveToLocalStorage(string key, string value);

        [DllImport("__Internal")]
        private static extern string LoadFromLocalStorage(string key);

        [DllImport("__Internal")]
        private static extern void RemoveFromLocalStorage(string key);

        [DllImport("__Internal")]
        private static extern int HasKeyInLocalStorage(string key);
        #endif
    }
}