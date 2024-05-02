using UnityEngine;

namespace Restaurant 
{
    [CreateAssetMenu(fileName = "DishesData", menuName = "Game Data/DishesData", order = 2)]
    public class DishesData : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
        public int Price;
        public float CookingTime;
    }
}

