using UnityEngine;
using UnityEngine.UI;

namespace QuestSystem.Dialogue
{
    public class Portrait : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void Show(Sprite image)
        {
            _image.gameObject.SetActive(true);
            _image.sprite = image;
        }

        public void Hide()
        {
            _image.gameObject.SetActive(false);
        }
    }
}