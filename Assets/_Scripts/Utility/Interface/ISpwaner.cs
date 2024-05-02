using UnityEngine;

namespace Restaurant
{
    public interface ISpwaner<T>
    {
        public abstract T Instantiate(string name = default, Vector3 position = default);
    }
}