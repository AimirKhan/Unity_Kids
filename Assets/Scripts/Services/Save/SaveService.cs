using Newtonsoft.Json;
using UnityEngine;

namespace Services.Save
{
    public class SaveService
    {
        public void Save<T>(string key, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
            Debug.Log($"[SaveService] Saved: {key}");
        }

        public T Load<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key)) return default;
            var json = PlayerPrefs.GetString(key);
            Debug.Log($"[SaveService] Loaded: {key}");
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        public void Delete(string key) =>  PlayerPrefs.DeleteKey(key);
    }
}