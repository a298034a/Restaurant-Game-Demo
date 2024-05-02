using Sirenix.OdinInspector;
using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "CustomerAsset", menuName = "Data Assest/ Customer", order = 2)]
    public class CustomerAsset : ScriptableObject
    {
        [TableList]
        public CustomerData[] Datas;

        public CustomerData GetData(string name)
        {
            foreach (CustomerData e in Datas)
            {
                if (e.Name == name) return e;
            }
            return new CustomerData();
        }
        public CustomerData GetRandomData()
        {
            int random = Random.Range(0, Datas.Length);

            return Datas[random];
        }

    }

    [System.Serializable]
    public struct CustomerData
    {
        [TableColumnWidth(30, Resizable = false)]
        public int ID;

        [TableColumnWidth(100, Resizable = false)]
        public string Name;

        [TableColumnWidth(100, Resizable = false)]
        public int Money;

        [TableColumnWidth(200, Resizable = false)]
        public RuntimeAnimatorController RuntimeAnimatorController;
    }

}