using UnityEngine;

namespace QuestSystem.Data.Quests
{
    [CreateAssetMenu(fileName = "Quest repository", menuName = "Game resources/Quests/Repository", order = 99)]
    public class QuestRepository : Repository<QuestDefinition>
    {
        public QuestDefinition GetRandomQuest()
        {
            int randomIndex = Random.Range(0, _defs.Count);
            return _defs[randomIndex];
        }
    }
}