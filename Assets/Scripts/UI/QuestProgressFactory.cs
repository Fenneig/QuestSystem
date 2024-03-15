using UnityEngine;

namespace QuestSystem.UI
{
    public class QuestProgressFactory
    {
        private QuestProgress _questProgressPrefab;

        public QuestProgressFactory(QuestProgress questProgressPrefab) => 
            _questProgressPrefab = questProgressPrefab;

        public QuestProgress Get(Transform container) => 
            GameObject.Instantiate(_questProgressPrefab, container);
    }
}
