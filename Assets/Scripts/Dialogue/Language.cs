using System;
using Fenneig_Dialogue_Editor.Runtime.Enums;

namespace QuestSystem.Dialogue
{
    public class Language
    {
        private LanguageType _currentLanguage;

        public LanguageType CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (_currentLanguage == value)
                    return;

                _currentLanguage = value;
                OnLanguageChanged?.Invoke();
            }
        }

        public event Action OnLanguageChanged;

        public Language(LanguageType baseLanguage) => _currentLanguage = baseLanguage;
    }
}