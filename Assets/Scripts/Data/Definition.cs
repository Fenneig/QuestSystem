using UnityEngine;

namespace QuestSystem.Data
{
    public abstract class Definition : ScriptableObject
    {
        [SerializeField] private string _id;

        public string ID => _id;
    }
}