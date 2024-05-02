using UnityEngine;

namespace Restaurant 
{
    public static class GameStaticDatabase
    {
        //Dishes
        public static DishesDataOverview DishesDataOverview => _dishesDataOverview;
        public static DishesUnit DishesUnitPrefab => _dishesUnitPrefab;
        private static DishesUnit _dishesUnitPrefab = Resources.Load<DishesUnit>("Dishes/[DishesUnit]");
        private static DishesDataOverview _dishesDataOverview = Resources.Load<DishesDataOverview>("GameData/DishesData/_DishesDataOverview");

        //Customer
        public static Customer CustomerPrefab => _customerPrefab;
        public static CustomerAsset CustomerAsset => _customerAsset;
        private static Customer _customerPrefab = Resources.Load<Customer>("Customer/[Customer]");
        private static CustomerAsset _customerAsset = Resources.Load<CustomerAsset>("CustomerAsset");

        //Furniture
        public static BuildUnit BuildUnitPrefab => _buildUnitPrefab;
        public static FurnitureDataOverview FurnitureDataOverview => _furnitureDataOverview;

        private static BuildUnit _buildUnitPrefab = Resources.Load<BuildUnit>("Building/[BuildUnit]");
        private static FurnitureDataOverview _furnitureDataOverview = Resources.Load<FurnitureDataOverview>("GameData/FurnitureData/_FurnitureDataOverview");
    }
}
