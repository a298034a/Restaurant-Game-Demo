using Restaurant;
using UnityEngine;

namespace Restaurant 
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameController _controller;
        void Start()
        {
            BuildingManager.Ins.Init();

            _controller.Init();
            Init();
        }
        public void Init()
        {
            _controller.BuildBtnOnClick(() => { BuildingManager.Ins.SwitchOnBuildingFunction(); });
            _controller.BusinessBtnOnClick(() => { BusinessManager.Ins.StartBusiness(); });
            _controller.CloseBtnOnClick(() => { BuildingManager.Ins.SwitchOffBuildingFunction(); });
        }
    }
}
