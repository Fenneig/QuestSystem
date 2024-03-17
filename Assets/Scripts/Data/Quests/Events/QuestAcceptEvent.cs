using System;
using Fenneig_Dialogue_Editor.Runtime.SO;
using UnityEngine;

namespace QuestSystem.Data.Quests.Events
{
    [CreateAssetMenu(fileName = "QuestAcceptEvent", menuName = "Dialogue/Events/QuestAccept")]
    public class QuestAcceptEvent : DialogueEventSO
    {
        public event Action OnQuestAccepted;

        public override void RunEvent() => 
            OnQuestAccepted?.Invoke();
    }
}