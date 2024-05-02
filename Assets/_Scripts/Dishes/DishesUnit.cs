using UnityEngine;

namespace Restaurant
{
    public class DishesUnit : MonoBehaviour
    {
        public Counter Counter => _counter;

        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Sprite _emptyPlate;

        private DishesData _data;
        private Counter _counter;

        public void Init(DishesData data)
        {
            _data = data;
            _sprite.sprite = data.Sprite;
        }
        public void SetPostion(Vector3 vector3)
        {
            this.transform.position = vector3;
        }
        public void SetLayer(int index)
        {
            _sprite.sortingOrder = index;
        }
        public void SetCounter(Counter counter)
        {
            _counter = counter;
        }
        public void BeingServed()
        {
            gameObject.SetActive(false);
            _counter.Occupied = false;
            _counter = null;
        }
        public void Delivered()
        {
            gameObject.SetActive(true);
        }
        public void Consumed()
        {
            _sprite.sprite = _emptyPlate;
        }
    }
}

