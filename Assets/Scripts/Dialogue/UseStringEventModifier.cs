using Fenneig_Dialogue_Editor.Runtime.Enums;

namespace QuestSystem.Dialogue
{
    public static class UseStringEventModifier
    {
        public static bool ModifierBoolCheck(StringEventModifierType eventModifier)
        {
            return eventModifier switch
            {
                StringEventModifierType.SetTrue => true,
                StringEventModifierType.SetFalse => false,
                _ => false
            };
        }
        
        public static float ModifierFloatCheck(float inputValue, StringEventModifierType eventModifier)
        {
            return eventModifier switch
            {
                StringEventModifierType.Add => inputValue,
                StringEventModifierType.Subtract => -inputValue,
                _ => 0
            };
        }
    }
}