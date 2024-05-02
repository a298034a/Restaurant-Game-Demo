using UnityEngine;

namespace Restaurant.Ingredient
{
    [CreateAssetMenu(fileName = "IngredientAsset", menuName = "Data Assest/ Ingredient", order = 1)]
    public class IngredientAsset : ScriptableObject
    {
        public Data[] Datas;

        public void Init(int length)
        {
            Datas = new Data[length];
        }

    }

    [System.Serializable]
    public struct Data
    {
        public int ID;
        public string Name;
        public int Charge;
        public int Price;
        public int Ground;
        public int Water;
        public int Fire;
        public int Wind;
        public int Days;
    }
}
