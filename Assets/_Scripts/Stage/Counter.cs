using UnityEngine;

namespace Restaurant
{
    [System.Serializable]
    public class Counter
    {
        public Vector3[] Positions;//角色導航位置
        public Vector3 PlacingPosition;//放置物品的座標
        public bool Occupied;//檯面已被放置物品
        public StageBuidUnitInfo StageBuidUnitInfo;
    }
}