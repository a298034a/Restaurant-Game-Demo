using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private Button _buildingBtn;

    [SerializeField]
    private Button _businessBtn;

    [SerializeField]
    private Button _closeBtn;
    public void Init()
    {
        Display(true);
    }
    public void BusinessBtnOnClick(UnityAction action)
    {
        _businessBtn.onClick.AddListener(action);
        _businessBtn.onClick.AddListener(() => { Display(false); });
    }
    public void BuildBtnOnClick(UnityAction action)
    {
        _buildingBtn.onClick.AddListener(action);
        _buildingBtn.onClick.AddListener(() => { Display(false); });
    }
    public void CloseBtnOnClick(UnityAction action) 
    {
        _closeBtn.onClick.AddListener(action);
        _closeBtn.onClick.AddListener(()=> { Display(true); });
    }
    public void Display(bool active)
    {
        _canvasGroup.alpha = active ? 1 : 0;
        _canvasGroup.interactable = active;
        _canvasGroup.blocksRaycasts = active;

        _closeBtn.gameObject.SetActive(!active);
    }
}
