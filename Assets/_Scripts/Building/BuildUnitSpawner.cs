using UnityEngine;

namespace Restaurant
{
    public class BuildUnitSpawner : ISpwaner<BuildUnit>
    {
        public BuildUnit Instantiate(string name, Vector3 position)
        {
            BuildUnit buildUnit = Object.Instantiate(GameStaticDatabase.BuildUnitPrefab, position, Quaternion.identity);
            FurnitureData data = GameStaticDatabase.FurnitureDataOverview.GetData(name);
            buildUnit.Init(data);

            return buildUnit;
        }
    }
}