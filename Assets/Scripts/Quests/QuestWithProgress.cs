using System;

namespace QuestSystem.Quests
{
    public class QuestWithProgress : IQuest
    {
        public string Id { get; }
        public event Action<string> OnQuestUpdated;
        public event Action OnQuestComplete;
        public event Action OnQuestFailed;
        public event Action OnQuestOver;

        private string _description;
        private int _maxAmount;
        private int _currentAmount;

        public QuestWithProgress(string description, int maxAmount)
        {
            _description = description;
            _maxAmount = maxAmount;
            _currentAmount = 0;
        }

        public void UpdateQuest(int newAmount)
        {
            _currentAmount = newAmount;
            
            if (_currentAmount < 0)
                _currentAmount = 0;

            if (_currentAmount > _maxAmount)
            {
                _currentAmount = _maxAmount;
                OnQuestComplete?.Invoke();
            }

            OnQuestUpdated?.Invoke(FormQuestInfo());
        }

        public void FailQuest() => OnQuestFailed?.Invoke();

        public void OverQuest() => OnQuestOver?.Invoke();

        private string FormQuestInfo() => $"{_description} {_currentAmount}/{_maxAmount}";
    }
}