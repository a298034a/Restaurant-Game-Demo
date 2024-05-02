using UnityEngine;

namespace Restaurant 
{
    [System.Serializable]
    public struct Seat
    {
        public Vector3Int Position;
        public Transform Transform;
        public int Direction;
        public StageBuidUnitInfo TableUnitInfo;
    }
}