using UnityEngine;

namespace QuestSystem.Data.Quests
{
    public abstract class QuestDefinition : Definition
    {
        [Header("Quest info")]
        [SerializeField] private string _questName;
        [SerializeField] private string _questDescription;
    }
}