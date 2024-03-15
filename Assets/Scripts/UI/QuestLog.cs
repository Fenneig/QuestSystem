using System.Collections.Generic;
using QuestSystem.Quests;
using UnityEngine;

namespace QuestSystem.UI
{
    public class QuestLog : MonoBehaviour
    {
        [SerializeField] private Transform _questContainer;
        [SerializeField] private QuestProgress _questProgressPrefab;

        private QuestProgressFactory _questProgressFactory;
        private List<IQuest> _questsList;

        public void SetupQuest(IQuest quest)
        {
            _questsList.Add(quest);
            
            QuestProgress questProgress = _questProgressFactory.Get(_questContainer);
            questProgress.SetName(quest.Id);
            quest.OnQuestUpdated += questProgress.UpdateProgress;
            quest.OnQuestComplete += questProgress.CompleteQuest;
            quest.OnQuestFailed += questProgress.FailQuest;
            quest.OnQuestOver += () => RemoveQuest(questProgress, quest);
        }

        private void RemoveQuest(QuestProgress questProgress, IQuest quest)
        {
            _questsList.Remove(quest);
            Destroy(questProgress);
        }

        private void Awake() => _questProgressFactory = new QuestProgressFactory(_questProgressPrefab);
    }
}