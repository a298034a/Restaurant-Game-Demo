using UnityEngine;

namespace Restaurant
{
    public class BuildingManager
    {
        private static BuildingManager _instance;

        private BuildingSystem _system;
        private BuildingController _controller;

        private FurnitureButton _button;

        public BuildingManager()
        {
            _system = GameObject.Find("BuildingSystem").GetComponent<BuildingSystem>();

            BuildUnitSpawner buildUnitSpawner = new BuildUnitSpawner();
            _system.Init(buildUnitSpawner);

            _controller = GameObject.Find("BuildingController").GetComponent<BuildingController>();

            _button = Resources.Load<FurnitureButton>("Building/[FurnitureButton]");
        }
        public void Init()
        {
            _controller.Display(false);
        }
        public void SetSystemModeToRemove()
        {
            _system.Mode = Mode.Remove;
        }
        public void SetSystemModeToMove()
        {
            _system.Mode = Mode.Move;
        }
        public void SetSystemModeToBuildAndSetUnit(string name)
        {
            _system.InstantiateFurniture(name);
            _system.Mode = Mode.Build;
        }
        public void SwitchOnBuildingFunction()
        {
            _controller.Display(true);
            _system.DisplayMainTilemap(true);
        }
        public void SwitchOffBuildingFunction()
        {
            _controller.Display(false);
            _system.DisplayMainTilemap(false);

            var graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
        }
        public static BuildingManager Ins
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BuildingManager();
                }
                return _instance;
            }
        }
    }
    public enum Mode
    {
        None,
        Build,
        Remove,
        Move
    }
}