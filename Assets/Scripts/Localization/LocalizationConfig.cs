using System;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    [CreateAssetMenu(fileName = "LocalizationConfig", menuName = "Localization/Localization Config", order = 66)]
    public class LocalizationConfig : ScriptableObject
    {
        [SerializeField] private List<LocaleData> locales = new();
        public List<LocaleData> Locales => locales;
        
        [Serializable]
        public class LocaleData
        {
            public string LanguageCode;
            public List<TranslationEntry> Entries = new();
        }
        
        [Serializable]
        public class TranslationEntry
        {
            public string Key;
            public string Value;
        }
    }

    

    
}