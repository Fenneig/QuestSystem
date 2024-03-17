using Fenneig_Dialogue_Editor.Runtime.SO;
using QuestSystem.Data.Quests.Events;
using UnityEngine;

namespace QuestSystem.Data.Quests
{
    public abstract class QuestDefinition : Definition
    {
        [Header("Quest info")]
        [SerializeField] private string _questName;
        [SerializeField] private string _questDescription;
        [Header("Dialogues")]
        [SerializeField] private DialogueContainerSO _startQuestDialogue;
        [SerializeField] private DialogueContainerSO _inprogressQuestDialogue;
        [Header("Events")]
        [SerializeField] private QuestAcceptEvent _acceptQuestEvent;
        [SerializeField] private QuestCompleteEvent _completeQuestEvent;

        public string QuestName => _questName;
        public string QuestDescription => _questDescription;
        public DialogueContainerSO StartQuestDialogue => _startQuestDialogue;
        public DialogueContainerSO InprogressQuestDialogue => _inprogressQuestDialogue;
        public QuestAcceptEvent AcceptQuestEvent => _acceptQuestEvent;
        public QuestCompleteEvent CompleteQuestEvent => _completeQuestEvent;
    }
}