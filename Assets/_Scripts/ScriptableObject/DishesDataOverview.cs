using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "DishesDataOverview", menuName = "Game Data/DishesDataOverview", order = 2)]
    public class DishesDataOverview : ScriptableObject
    {
        [SerializeField]
        private List<DishesData> _datas;

        public DishesData GetData(string name)
        {
            DishesData data = _datas.Find(x => x.Name == name);

            return data;
        }
        public string GetRandomDishesName()
        {
            int randomIndex = Random.Range(0, _datas.Count);
            DishesData data = _datas[randomIndex];

            return data.Name;
        }
        private List<string> _dishesNames = new List<string>();
        public List<string> GetDishesNames()
        {
            _dishesNames.Clear();

            foreach (DishesData data in _datas)
            {
                _dishesNames.Add(data.Name);
            }

            return _dishesNames;
        }
    }
}

