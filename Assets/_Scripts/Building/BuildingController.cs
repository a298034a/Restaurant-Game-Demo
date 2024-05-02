using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Restaurant
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _layout;
        [SerializeField] private Button _removeBtn;
        [SerializeField] private Button _moveBtn;
        [SerializeField] private FurnitureDataOverview _furnitureDataOverview;

        private FurnitureButton _prefab;

        private void Start()
        {
            _prefab = Resources.Load<FurnitureButton>("Building/[FurnitureButton]");

            //furniture source will replace by stage info
            foreach (var name in _furnitureDataOverview.GetFurnitureNames())
            {
                FurnitureButton button = Instantiate(_prefab);
                button.Init(name,()=> { BuildingManager.Ins.SetSystemModeToBuildAndSetUnit(name); });
                button.transform.SetParent(_layout, false);
            }

            _removeBtn.onClick.AddListener(() =>
            { BuildingManager.Ins.SetSystemModeToRemove(); });

            _moveBtn.onClick.AddListener(() =>
            { BuildingManager.Ins.SetSystemModeToMove(); });
        }
        public void Display(bool active) 
        {
            _canvasGroup.alpha = active ? 1 : 0;
            _canvasGroup.interactable = active;
            _canvasGroup.blocksRaycasts = active;
        }
    }
}