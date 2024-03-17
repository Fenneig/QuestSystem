using System.Collections.Generic;
using System.Linq;
using Fenneig_Dialogue_Editor.Runtime.Enums;
using Fenneig_Dialogue_Editor.Runtime.SO.Dialogue;
using QuestSystem.Data.Quests;
using QuestSystem.Interacts;
using QuestSystem.UI.Dialogue;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace QuestSystem.Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueTalk : DialogueGetData, IInteractable
    {
        [SerializeField] private GameObject _hintCanvas;
        private DialogueWidget _dialogueWidget;
        private AudioSource _audioSource;

        private DialogueData _currentDialogueNodeData;
        private DialogueData _lastDialogueNodeData;

        private List<DialogueDataBaseContainer> _dialogueNodes;
        private int _currentIndex = 0;
        private GameEventSystem _gameEventSystem;
        private Language _language;
        private QuestDefinition _quest;
        private bool _isQuestAccepted;

        public bool CanInteract { get; private set; }
        
        [Inject]
        private void Construct(GameEventSystem gameEventSystem, Language language, DialogueWidget dialogueWidget)
        {
            _gameEventSystem = gameEventSystem;
            _language = language;
            _dialogueWidget = dialogueWidget;
        }

        public void SetupQuest(QuestDefinition quest)
        {
            _quest = quest;
            _quest.AcceptQuestEvent.OnQuestAccepted += OnQuestAccepted;
            CanInteract = true;
            _isQuestAccepted = false;
        }

        private void StartDialogue()
        {
            _dialogueWidget.ShowDialogue();
            DialogueContainer = _isQuestAccepted ? _quest.InprogressQuestDialogue : _quest.StartQuestDialogue;
            CheckNodeType(GetNextNode(DialogueContainer.StartData[0]));
        }

        private void OnQuestAccepted()
        {
            _isQuestAccepted = true;
            _quest.AcceptQuestEvent.OnQuestAccepted -= OnQuestAccepted;
        }


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void CheckNodeType(BaseData baseNodeData)
        {
            switch (baseNodeData)
            {
                case StartData nodeData:
                    RunNode(nodeData);
                    break;
                case DialogueData nodeData:
                    RunNode(nodeData);
                    break;
                case EventData nodeData:
                    RunNode(nodeData);
                    break;
                case EndData nodeData:
                    RunNode(nodeData);
                    break;
                case BranchData nodeData:
                    RunNode(nodeData);
                    break;
            }
        }

        private void RunNode(StartData nodeData) => 
            CheckNodeType(GetNextNode(DialogueContainer.StartData[0]));

        private void RunNode(DialogueData nodeData)
        {
            _currentDialogueNodeData = nodeData;

            _dialogueNodes = new List<DialogueDataBaseContainer>();
            _dialogueNodes.AddRange(nodeData.DialogueDataImages);
            _dialogueNodes.AddRange(nodeData.DialogueDataNames);
            _dialogueNodes.AddRange(nodeData.DialogueDataTexts);

            _currentIndex = 0;
            
            _dialogueNodes.Sort((x,y) => x.ID.Value.CompareTo(y.ID.Value));

            DoTheDialogue();
        }

        private void DoTheDialogue()
        {
            _dialogueWidget.HideButtons();

            for (int i = _currentIndex; i < _dialogueNodes.Count; i++)
            {
                _currentIndex = i + 1;
                if (_dialogueNodes[i] is DialogueDataName dataName)
                {
                    _dialogueWidget.SetName(dataName.CharacterName.Find(text => text.LanguageType == _language.CurrentLanguage).LanguageGenericType);
                }
                else if (_dialogueNodes[i] is DialogueDataImage dataImage)
                {
                    _dialogueWidget.SetImage(dataImage.LeftSprite.Value, dataImage.RightSprite.Value);
                }
                else if (_dialogueNodes[i] is DialogueDataText dataText)
                {
                    _dialogueWidget.SetText(dataText.Text.Find(text => text.LanguageType == _language.CurrentLanguage).LanguageGenericType);
                    ShowButtons();
                    PlayAudio(dataText.AudioClips.Find(clip => clip.LanguageType == _language.CurrentLanguage).LanguageGenericType);
                    break;
                }
            }
        }

        private void ShowButtons()
        {
            List<DialogueButtonContainer> dialogueButtonContainers = new();
            if (_currentIndex == _dialogueNodes.Count)
            {
                if (_currentDialogueNodeData.DialogueDataPorts.Count == 0)
                {
                    void OnUnityAction() => CheckNodeType(GetNextNode(_currentDialogueNodeData));
                    dialogueButtonContainers.Add(GetContinueButton(OnUnityAction));
                    _dialogueWidget.SetupButtons(dialogueButtonContainers);
                }
                else
                {
                    _currentDialogueNodeData.DialogueDataPorts.ForEach(port => ChoiceCheck(port.InputGuid, dialogueButtonContainers));
                    _dialogueWidget.SetupButtons(dialogueButtonContainers);
                }
            }
            else
            {
                dialogueButtonContainers.Add(GetContinueButton(DoTheDialogue));
                _dialogueWidget.SetupButtons(dialogueButtonContainers);
            }
        }

        private DialogueButtonContainer GetContinueButton(UnityAction unityAction)
        {
            DialogueButtonContainer buttonContainer = new DialogueButtonContainer()
            {
                Text = "Continue",
                ConditionCheck = true,
                UnityAction = unityAction,
                ChoiceStateType = default
            };
            return buttonContainer;
        }

        private void ChoiceCheck(string guidID, List<DialogueButtonContainer> dialogueButtonContainers)
        {
            ChoiceData choiceNode = GetNodeByGuid(guidID) as ChoiceData;
            DialogueButtonContainer dialogueButtonContainer = new();

            bool checkBranch = choiceNode != null && choiceNode.EventDataStringConditions.All(item => 
                _gameEventSystem.ConditionEvent(item.StringEventText.Value, item.StringEventConditionType.Value, item.Number.Value));

            void OnUnityAction() => CheckNodeType(GetNextNode(choiceNode));

            dialogueButtonContainer.ChoiceStateType = choiceNode.ChoiceStateType.Value;
            dialogueButtonContainer.Text = choiceNode.Text
                .Find(text => text.LanguageType == _language.CurrentLanguage).LanguageGenericType;
            dialogueButtonContainer.UnityAction = OnUnityAction;
            dialogueButtonContainer.ConditionCheck = checkBranch;

            dialogueButtonContainers.Add(dialogueButtonContainer);
        }

        private void PlayAudio(AudioClip audioClip)
        {
            _audioSource.Stop();
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        private void RunNode(EventData nodeData)
        {
            nodeData.ContainerDialogueEventSOs.ForEach(item => item.DialogueEventSO.RunEvent());
            nodeData.EventDataStringModifiers.ForEach(item =>
                _gameEventSystem.ModifierEvent(item.StringEventText.Value, item.StringEventModifierType.Value, item.Number.Value));
            
            CheckNodeType(GetNextNode(nodeData));
        }

        private void RunNode(EndData nodeData)
        {
            switch (nodeData.EndNodeType.Value)
            {
                case EndNodeType.End:
                    _dialogueWidget.HideDialogue();
                    break;
                case EndNodeType.Repeat:
                    CheckNodeType(GetNodeByGuid(_currentDialogueNodeData.NodeGuid));
                    break;
                case EndNodeType.ReturnToStart:
                    CheckNodeType(GetNextNode(DialogueContainer.StartData[0]));
                    break;
            }
        }

        private void RunNode(BranchData nodeData)
        {
            bool checkBranch = nodeData.EventDataStringConditions.All(item => 
                _gameEventSystem.ConditionEvent(item.StringEventText.Value, item.StringEventConditionType.Value, item.Number.Value));

            string nextNode = checkBranch ? nodeData.TrueGuidNode : nodeData.FalseGuidNode;
            CheckNodeType(GetNodeByGuid(nextNode));
        }


        public void Interact()
        {
            if (CanInteract)
                StartDialogue();
        }

        public void ShowHint()
        {
            if (CanInteract)
                _hintCanvas.SetActive(true);
        }

        public void HideHint()
        {
            _hintCanvas.SetActive(false);
        }
    }
}