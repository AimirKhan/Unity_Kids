namespace Localization
{
    public class LocalizationService
    {
        private readonly LocalizationConfig config;
        private string currentLanguage = "en";
        
        public LocalizationService(LocalizationConfig locConfig) => config = locConfig;

        public string GetText(string key)
        {
            LocalizationConfig.LocaleData targetLocale = null;
            for (int i = 0; i < config.Locales.Count; i++)
            {
                if (config.Locales[i].LanguageCode == currentLanguage)
                {
                    targetLocale = config.Locales[i];
                    break;
                }
            }
            
            if (targetLocale == null) return "LOC_NOT_FOUND";

            for (int j = 0; j < targetLocale.Entries.Count; j++)
            {
                if (targetLocale.Entries[j].Key == key)
                {
                    return targetLocale.Entries[j].Value;
                }
            }
            
            return "MISSING: " + key;
        }
    }
}