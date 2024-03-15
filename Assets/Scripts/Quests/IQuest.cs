using System;

namespace QuestSystem.Quests
{
    public interface IQuest
    {
        public string Id { get; }
        public event Action<string> OnQuestUpdated;
        public event Action OnQuestComplete;
        public event Action OnQuestFailed;
        public event Action OnQuestOver;
    }
}