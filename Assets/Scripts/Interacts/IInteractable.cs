namespace QuestSystem.Interacts
{
    public interface IInteractable
    {
        bool CanInteract { get; }
        void Interact();
        void ShowHint();
        void HideHint();
    }
}
