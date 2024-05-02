using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Restaurant
{
    public class BusinessController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Display(bool active)
        {
            _canvasGroup.alpha = active ? 1 : 0;
            _canvasGroup.interactable = active;
            _canvasGroup.blocksRaycasts = active;
        }
    }

}
