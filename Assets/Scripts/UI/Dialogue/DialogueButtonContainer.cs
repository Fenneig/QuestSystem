using Fenneig_Dialogue_Editor.Runtime.Enums;
using UnityEngine.Events;

namespace QuestSystem.UI.Dialogue
{
    public class DialogueButtonContainer
    {
        public UnityAction UnityAction { get; set; }
        public string Text { get; set; }
        public bool ConditionCheck { get; set; }
        public ChoiceStateType ChoiceStateType { get; set; }
    }
}