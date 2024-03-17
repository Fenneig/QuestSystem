using Fenneig_Dialogue_Editor.Runtime.Enums;
using UnityEngine;

namespace QuestSystem
{
    public class GameEventSystem
    {
        public bool ConditionEvent(string stringEvent, StringEventConditionType conditionType, float value = 0)
        {
            Debug.Log($"Check string event with type {conditionType} and value of {value}");
            return false;
        }
        
        public void ModifierEvent(string stringEvent, StringEventModifierType modifierType, float value = 0)
        {
            Debug.Log($"Modify string event with type {modifierType} and value of {value}");
        }
    }
}