using QuestSystem.Data.Quests;
using QuestSystem.Dialogue;
using UnityEngine;

namespace QuestSystem.Characters
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private QuestRepository _questRepository;
        [SerializeField] private DialogueTalk _dialogueTalk;

        private QuestDefinition _currentQuest;

        private void Awake()
        {
            _currentQuest = _questRepository.GetRandomQuest();
            _dialogueTalk.SetupQuest(_currentQuest);
        }

    }
}