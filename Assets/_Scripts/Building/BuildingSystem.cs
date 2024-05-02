using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

namespace Restaurant
{
    public class BuildingSystem : MonoBehaviour
    {
        public Mode Mode { get; set; }

        [SerializeField]
        private TilemapService _tilemap;

        private ISpwaner<BuildUnit> _buildUnitSpawner;

        //可替換為玩家位置等
        private Vector2 _touchPos;
        private Vector3Int _cellPos;

        private Vector3 _prevPos;
        private BoundsInt _prevArea;

        private BuildUnit _tempUnit;

        public void Init(BuildUnitSpawner buildUnitSpawner)
        {
            _buildUnitSpawner = buildUnitSpawner;
        }
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            switch (Mode)
            {
                case Mode.None:
                    break;
                case Mode.Build:
                    if (!_tempUnit) return;
                    _touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _cellPos = _tilemap.GetLocalToCellPosition(_touchPos);
                    Build();
                    break;

                case Mode.Remove:
                    _touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _cellPos = _tilemap.GetLocalToCellPosition(_touchPos);

                    if (Input.GetMouseButtonDown(0)) Remove(_cellPos);
                    break;

                case Mode.Move:
                    _touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    _cellPos = _tilemap.GetLocalToCellPosition(_touchPos);

                    if (!_tempUnit)
                    {
                        if (Input.GetMouseButtonDown(0)) Move(_cellPos);
                    }
                    else
                    {
                        Build();
                    }
                    break;
            }

            void Build()
            {
                if (_prevPos != _cellPos)
                {
                    _tempUnit.transform.localPosition = _tilemap.GetCellToLocalInterpolated(_cellPos
                            + new Vector3(0.5f, 0.5f, 0));
                    _prevPos = _cellPos;
                    RefreshTiles();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    _tempUnit.TurnRight();
                    RefreshTiles();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (CanPlace())
                    {
                        Place();
                    }
                }
            }
        }
        public void Place()
        {
            _touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _cellPos = _tilemap.GetLocalToCellPosition(_touchPos);
            _tempUnit.transform.localPosition = _tilemap.GetCellToLocalInterpolated(_cellPos
                    + new Vector3(0.5f, 0.5f, 0));

            Vector3Int positionInt = _tilemap.GetLocalToCellPosition(_tempUnit.transform.position);
            BoundsInt areaTemp = _tempUnit.Area;
            areaTemp.position = positionInt;

            _tilemap.SetTilesBlockToTempTilemap(areaTemp, TileType.Empty);
            _tilemap.SetTilesBlockToMainTilemap(areaTemp, TileType.Empty);
            _tilemap.SetTilesBlockToTempTilemap(_prevArea, TileType.Empty);

            RestaurantInfoProvider.Ins.Info.AddBuildUnit(positionInt, _tempUnit);
            _tempUnit = null;
        }
        public void Remove(Vector3Int position)
        {
            BuildUnit furniture = RestaurantInfoProvider.Ins.Info.GetBuildUnit(position);
            if (furniture == null) return;

            BoundsInt area = furniture.Area;
            _tilemap.SetTilesBlockToMainTilemap(area, TileType.White);

            RestaurantInfoProvider.Ins.Info.RemoveBuildUnit(position);
            Destroy(furniture.gameObject);
        }
        public void Move(Vector3Int position)
        {
            _tempUnit = RestaurantInfoProvider.Ins.Info.GetBuildUnit(position);
            if (_tempUnit == null) return;

            BoundsInt area = _tempUnit.Area;
            _tilemap.SetTilesBlockToMainTilemap(area, TileType.White);

            RestaurantInfoProvider.Ins.Info.RemoveBuildUnit(position);
        }
        public void InstantiateFurniture(string name)
        {
            if (_tempUnit != null)
            {
                _tilemap.SetTilesBlockToTempTilemap(_prevArea, TileType.Empty);
                Destroy(_tempUnit.gameObject);
            }

            _tempUnit = _buildUnitSpawner.Instantiate(name);
        }
        public bool CanPlace()
        {
            BoundsInt buildingArea = _tempUnit.Area;
            if (!_tilemap.PositionIsInMainTiles(buildingArea)) return false;
            if (!PositionIsEmpty()) return false;
            return true;

            bool PositionIsEmpty()
            {
                BoundsInt buildingArea = _tempUnit.Area;
                Vector3Int[] positionInt = _tilemap.GetTilesBlockPositionsInMainTiles(buildingArea);

                int length = positionInt.Length;

                for (int i = 0; i < length; i++)
                {
                    if (RestaurantInfoProvider.Ins.Info.GetBuildUnit(positionInt[i]) != null) return false;
                }
                return true;
            }
        }
        public void DisplayMainTilemap(bool active)
        {
            _tilemap.SetMainTilemapActive(active);
        }
        private void RefreshTiles()
        {
            _tilemap.SetTilesBlockToTempTilemap(_prevArea, TileType.Empty);
            _tempUnit.SetPostion(_tilemap.GetWorldToCellPaosition(_tempUnit.transform.position));
            BoundsInt buildingArea = _tempUnit.Area;

            int layer = (buildingArea.x + buildingArea.y) * -1;
            if (buildingArea.size.x > 1 && buildingArea.size.y > 1)
            {
                layer--;
            }
            _tempUnit.SetLayer(layer);

            if (CanPlace())
            {
                _tilemap.SetTilesBlockToTempTilemap(buildingArea, TileType.Green);
            }
            else
            {
                _tilemap.SetTilesBlockToTempTilemap(buildingArea, TileType.Red);
            }

            _prevArea = buildingArea;
        }
    }
    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }
}
