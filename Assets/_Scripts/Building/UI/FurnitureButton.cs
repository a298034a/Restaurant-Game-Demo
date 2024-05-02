using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Restaurant
{
    public class FurnitureButton : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Button _button;

        public void Init(string text, UnityAction action)
        {
            _name.text = text;
            _button.onClick.AddListener(action);
        }
    }
}
