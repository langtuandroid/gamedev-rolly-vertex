using UnityEngine;

    namespace Audio
    {
        public class Settings
        {
            public void SaveSettings(string key, bool state)
            {
                PlayerPrefs.SetInt(key, state ? 1 : 0);
                PlayerPrefs.Save();
            }
         
            public bool LoadSettings(string key)
            {
                if (!PlayerPrefs.HasKey(key))
                {
                    SaveSettings(key, false);
                    PlayerPrefs.Save();
                }
                return PlayerPrefs.GetInt(key) == 1;
            }
        
        }
    }
