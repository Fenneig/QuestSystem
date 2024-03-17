using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuestSystem.UI.Dialogue
{
    [RequireComponent(typeof(Button))]
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _buttonText;

        private Button _button;
        public TMP_Text ButtonText => _buttonText;

        private void Awake() => _button = GetComponent<Button>();

        public void SetText(string text) => _buttonText.text = text;

        public void TryInvoke()
        {
            if (_button.interactable && _button.isActiveAndEnabled)
                _button.onClick?.Invoke();
        }
    }
}