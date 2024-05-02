using UnityEngine;

namespace Restaurant
{
    [System.Serializable]
    public struct StageBuidUnitInfo
    {
        public string Name;
        public string Type;
        public int Direction;
        public Vector3Int[] Positions;
        public BuildUnit Unit;
    }
}

