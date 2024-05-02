using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Restaurant 
{
    public class TilemapService : MonoBehaviour
    {
        [SerializeField]
        private GridsInfo _gridsInfo;

        private Dictionary<TileType, TileBase> _tileBases;
        private Vector3Int[] _mainTiles;

        void Start()
        {
            _gridsInfo = GetComponent<GridsInfo>();

            _tileBases = new Dictionary<TileType, TileBase>
            {
                { TileType.Empty, null },
                { TileType.White, Resources.Load<TileBase>("Tiles/white") },
                { TileType.Green, Resources.Load<TileBase>("Tiles/green") },
                { TileType.Red, Resources.Load<TileBase>("Tiles/red") }
            };

            _mainTiles = _gridsInfo.GetMainTilemap().GetAllTiles();
        }
        public Vector3Int GetLocalToCellPosition(Vector3 pos) 
        {
            return _gridsInfo.GetGridLayout().LocalToCell(pos);
        }
        public Vector3 GetCellToLocalInterpolated(Vector3 pos) 
        {
            return _gridsInfo.GetGridLayout().CellToLocalInterpolated(pos);
        }
        public Vector3Int GetWorldToCellPaosition(Vector3 pos) 
        {
            return _gridsInfo.GetGridLayout().WorldToCell(pos);
        }
        public bool PositionIsInMainTiles(BoundsInt area) 
        {
            Vector3Int[] positionInt = _GetTilesBlockPositions(area, _gridsInfo.GetMainTilemap());

            int mainLength = _mainTiles.Length;
            int positionLength = positionInt.Length;
            bool[] results = new bool[positionLength];

            for (int i = 0; i < positionLength; i++)
            {
                for (int j = 0; j < mainLength; j++)
                {
                    if (positionInt[i] == _mainTiles[j])
                    {
                        results[i] = true;
                    }
                }
            }

            if (results.Contains(false)) return false;

            return true;
        }
        public Vector3Int[] GetTilesBlockPositionsInMainTiles(BoundsInt area) 
        {
            return _GetTilesBlockPositions(area, _gridsInfo.GetMainTilemap());
        }
        public void SetTilesBlockToMainTilemap(BoundsInt area, TileType type) 
        {
            _SetTilesBlock(area, type, _gridsInfo.GetMainTilemap());
        }
        public void SetTilesBlockToTempTilemap(BoundsInt area, TileType type)
        {
            _SetTilesBlock(area, type, _gridsInfo.GetTempTilemap());
        }
        public void SetMainTilemapActive(bool active) 
        {
            _gridsInfo.GetMainTilemap().gameObject.SetActive(active);
        }
        private void _SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
        {
            int size = area.size.x * area.size.y * area.size.z;
            TileBase[] tileArray = new TileBase[size];
            FillTiles(tileArray, type);
            tilemap.SetTilesBlock(area, tileArray);
        }
        public void FillTiles(TileBase[] arr, TileType type)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = _tileBases[type];
            }
        }
        private Vector3Int[] _GetTilesBlockPositions(BoundsInt area, Tilemap tilemap)
        {
            TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
            Vector3Int[] positions = new Vector3Int[array.Length];

            int counter = 0;
            foreach (var v in area.allPositionsWithin)
            {
                Vector3Int pos = new Vector3Int(v.x, v.y, z: 0);
                positions[counter] = pos;
                counter++;
            }

            return positions;
        }
    }
}