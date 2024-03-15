using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuestSystem.UI
{
    public class QuestProgress : MonoBehaviour
    {
        [SerializeField] private TMP_Text _questName;
        [SerializeField] private Image _markImage;
        [SerializeField] private TMP_Text _progress;
        [Header("UI sprites")]
        [SerializeField] private Sprite _successMark;
        [SerializeField] private Sprite _failMark;
        [Header("Colors")]
        [SerializeField] private Color _successColor;
        [SerializeField] private Color _failColor;

        public void SetName(string questName) =>
            _questName.text = questName;

        public void UpdateProgress(string progress) => 
            _progress.text = progress;

        public void FailQuest()
        {
            _markImage.gameObject.SetActive(true);
            _markImage.sprite = _failMark;
            _markImage.color = _failColor;
        }

        public void CompleteQuest()
        {
            _markImage.gameObject.SetActive(true);
            _markImage.sprite = _successMark;
            _markImage.color = _successColor;
        }
    }
}