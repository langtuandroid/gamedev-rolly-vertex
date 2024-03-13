using System.Linq;
using UnityEngine;

    namespace Audio
    {
        public class Settings
        {
            public void SaveSettingsrv(string key, bool state)
            {
                PlayerPrefs.SetInt(key, state ? 1 : 0);
                PlayerPrefs.Save();
            }
         
            public bool LoadSettingsrv(string key)
            {
                if (!PlayerPrefs.HasKey(key))
                {
                    SaveSettingsrv(key, false);
                    PlayerPrefs.Save();
                }
                return PlayerPrefs.GetInt(key) == 1;
            }
            
            private int CalculateArraySumrv(int[] array)
            {
                return array.Sum();
            }
        
        }
    }
