using UnityEngine;

namespace QuestSystem.Data.Quests
{
    [CreateAssetMenu(fileName = "Quest with progress", menuName = "Game resources/Quests/Quest with progress")]
    public class QuestWithProgressDefinition : QuestDefinition
    {
        [Space]
        [SerializeField] private int _maxProgress;

        public int MaxProgress => _maxProgress;
    }
}