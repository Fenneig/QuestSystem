using System;
using Fenneig_Dialogue_Editor.Runtime.SO;
using UnityEngine;

namespace QuestSystem.Data.Quests.Events
{
    [CreateAssetMenu(fileName = "QuestCompleteEvent", menuName = "Dialogue/Events/QuestComplete")]
    public class QuestCompleteEvent : DialogueEventSO
    {
        public event Action OnQuestComplete;

        public override void RunEvent() => 
            OnQuestComplete?.Invoke();
    }
}