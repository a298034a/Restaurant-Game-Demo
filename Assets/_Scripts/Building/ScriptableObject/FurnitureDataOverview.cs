using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "FurnitureDataOverview", menuName = "Game Data/FurnitureDataOverview", order = 1)]
    public class FurnitureDataOverview : ScriptableObject
    {
        [SerializeField]
        private List<FurnitureData> _datas;

        public FurnitureData GetData(string name)
        {
            FurnitureData data = _datas.Find(x => x.Name == name);

            return data;
        }

        private List<string> _furnitureNames = new List<string>();
        public List<string> GetFurnitureNames() 
        {
            _furnitureNames.Clear();

            foreach (FurnitureData data in _datas)
            {
                _furnitureNames.Add(data.Name);
            }

            return _furnitureNames;
        }
    }
}
