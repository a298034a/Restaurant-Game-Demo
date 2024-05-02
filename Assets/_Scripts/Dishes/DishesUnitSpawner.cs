using UnityEngine;

namespace Restaurant
{
    public class DishesUnitSpawner : ISpwaner<DishesUnit>
    {
        public DishesUnit Instantiate(string name, Vector3 position)
        {
            DishesUnit dishesUnit = Object.Instantiate(GameStaticDatabase.DishesUnitPrefab, position, Quaternion.identity);
            DishesData data = GameStaticDatabase.DishesDataOverview.GetData(name);
            dishesUnit.Init(data);

            return dishesUnit;
        }
    }
}

