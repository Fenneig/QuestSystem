using Fenneig_Dialogue_Editor.Runtime.Enums;
using QuestSystem.Dialogue;
using UnityEngine;
using Zenject;

namespace QuestSystem.DI
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private LanguageType _baseLanguage;
        
        public override void InstallBindings()
        {
            Container.Bind<Language>().AsSingle().WithArguments(_baseLanguage);

        }
    }
}