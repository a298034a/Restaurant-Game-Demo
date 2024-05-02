using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridsInfo : MonoBehaviour
{
    [Title("Tilemap")]
    [SerializeField]
    private GridLayout _gridLayout;
    [SerializeField]
    private Tilemap _mainTilemap;
    [SerializeField]
    private Tilemap _tempTilemap;

    public GridLayout GetGridLayout() 
    {
        return _gridLayout;
    }
    public Tilemap GetMainTilemap() 
    {
        return _mainTilemap;
    }
    public Tilemap GetTempTilemap()
    {
        return _tempTilemap;
    }
}
