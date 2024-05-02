using UnityEngine;

namespace Restaurant
{
    public class CustomerSpawner : ISpwaner<Customer>
    {
        public Customer Instantiate(string name, Vector3 position)
        {
            Customer customer = Object.Instantiate(GameStaticDatabase.CustomerPrefab, position, Quaternion.identity);
            CustomerData data = GameStaticDatabase.CustomerAsset.GetData(name);
            customer.Init(data);

            return customer;
        }
        public Customer InstantiateRandom(Vector3 position)
        {
            Customer customer = Object.Instantiate(GameStaticDatabase.CustomerPrefab, position, Quaternion.identity);
            CustomerData data = GameStaticDatabase.CustomerAsset.GetRandomData();
            customer.Init(data);

            return customer;
        }
    }
}
