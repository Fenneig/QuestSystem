using QuestSystem.UI.Dialogue;
using UnityEngine;
using Zenject;

namespace QuestSystem.DI
{
    public class DialogueWidgetInstaller : MonoInstaller
    {
        [SerializeField] private DialogueWidget _dialogueWidget;

        public override void InstallBindings()
        {
            Container.Bind<DialogueWidget>().FromInstance(_dialogueWidget).AsSingle();
        }
    }
}