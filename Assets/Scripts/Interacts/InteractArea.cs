using UnityEngine;

namespace QuestSystem.Interacts
{
    public class InteractArea : MonoBehaviour
    {
        private IInteractable _interactable;

        public void Interact() => _interactable?.Interact();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable) == false)
                return;

            _interactable = interactable;
            _interactable.ShowHint();
        }

        private void OnTriggerExit(Collider other)
        {
            _interactable?.HideHint();
            _interactable = null;
        }
    }
}