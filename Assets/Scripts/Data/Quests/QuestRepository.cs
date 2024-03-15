using UnityEngine;

namespace QuestSystem.Data.Quests
{
    [CreateAssetMenu(fileName = "Quest repository", menuName = "Game resources/Quests/Repository", order = 99)]
    public class QuestRepository : Repository<QuestDefinition>
    {
    }
}