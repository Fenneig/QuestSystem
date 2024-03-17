using System.Collections.Generic;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using QuestSystem.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace QuestSystem.UI.Dialogue
{
    public class DialogueWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _dialoguePanel;
        [Header("Text")]
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _dialogueText;
        [Header("Image")] 
        [SerializeField] private Portrait _leftImage;
        [SerializeField] private Portrait _rightImage;
        [Header("Button")]
        [SerializeField] private GameObject _buttonContentPanel;
        [SerializeField] private ButtonController _buttonPrefab;

        private List<ButtonController> _buttons = new();

        public void ShowDialogue() => _dialoguePanel.SetActive(true);
        public void HideDialogue() => _dialoguePanel.SetActive(false);

        public void SetName(string nameText) => _nameText.text = nameText;

        public void SetText(string text) => _dialogueText.text = text;

        public void SetupButtons(List<DialogueButtonContainer> buttonContainers)
        {
            HideButtons();
            CheckAndFillDialogueAnswerRooms(buttonContainers.Count);
            int hideOutButtons = 0;
            for (int i = 0; i < buttonContainers.Count; i++)
            {
                if (buttonContainers[i].ConditionCheck == false && buttonContainers[i].ChoiceStateType == ChoiceStateType.Hide)
                {
                    hideOutButtons++;
                    continue;
                }
                Button currentButton = _buttons[i].GetComponent<Button>();
                currentButton.onClick = new Button.ButtonClickedEvent();
                currentButton.interactable = true;
                if (buttonContainers[i].ConditionCheck || buttonContainers[i].ChoiceStateType == ChoiceStateType.GrayOut)
                {
                    _buttons[i].SetText($"{i + 1}: {buttonContainers[i].Text}");
                    _buttons[i].gameObject.SetActive(true);

                    if (!buttonContainers[i].ConditionCheck)
                    {
                        currentButton.interactable = false;
                        var colors = currentButton.colors;
                        currentButton.colors = colors;
                    }
                    else
                    {
                        currentButton.onClick.AddListener(buttonContainers[i].UnityAction);
                    }
                }
            }
        }

        public void SetImage(Sprite leftImage, Sprite rightImage)
        { 
            _leftImage.Hide();
            _rightImage.Hide();

            if (leftImage != null) 
                _leftImage.Show(leftImage);
            
            if (rightImage != null)
                _rightImage.Show(rightImage);
        }

        public void HideButtons() => _buttons.ForEach(button => button.gameObject.SetActive(false));

        private void Update()
        {
            for (int i = 0; i < _buttons.Count; i++)
                if (Keyboard.current[Key.Digit1 + i].wasPressedThisFrame)
                    _buttons[i].TryInvoke();
        }

        private void CheckAndFillDialogueAnswerRooms(int buttonsCountToCreate)
        {
            if (_buttons.Count >= buttonsCountToCreate) return;
            int buttonsToCreate = buttonsCountToCreate - _buttons.Count;
            for (int i = 0; i < buttonsToCreate; i++)
            {
                ButtonController button = Instantiate(_buttonPrefab, _buttonContentPanel.transform);
                _buttons.Add(button);
                button.gameObject.SetActive(false);
            }
        }
    }
}