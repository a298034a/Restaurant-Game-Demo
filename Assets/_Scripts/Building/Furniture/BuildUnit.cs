using UnityEngine;

namespace Restaurant
{
    public class BuildUnit : MonoBehaviour
    {
        public BoundsInt Area => _area;
        public FurnitureData Data => _data;
        public int Direction => _direction;

        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private BoundsInt _area;
        [SerializeField] private FurnitureData _data;

        private int _direction;

        public void Init(FurnitureData data)
        {
            _data = data;

            gameObject.name = '[' + data.Name + ']';

            _area.size = _data.Size;
            _sprite.sprite = _data.Sprites[0];
            _collider.offset = new Vector2();

            FixSpritePosition();

            FixColliderParameter();

            void FixSpritePosition()
            {
                if (_area.size.x == 2 && _area.size.y == 2)
                {
                    _sprite.gameObject.transform.position += new Vector3(0.5f, 0.5f, 0);
                }
            }

            void FixColliderParameter()
            {
                if (_area.size.x == 2 && _area.size.y == 2)
                {
                    _collider.offset = new Vector2(0.5f, 0.5f);
                    _collider.size = new Vector2(2, 2);
                }
                else if (_area.size.x == 3 && _area.size.y == 1)
                {
                    _collider.offset = new Vector2(1, 0);
                    _collider.size = new Vector2(3, 1);
                }
            }
        }
        public void SetPostion(Vector3Int vector3)
        {
            _area.position = vector3;
        }
        public void SetLayer(int index)
        {
            _sprite.sortingOrder = index;
        }
        public void TurnRight()
        {
            if (_direction < 3)
            {
                _direction++;
            }
            else _direction = 0;

            _area.size = new Vector3Int(_area.size.y, _area.size.x, 1);

            _sprite.sprite = _data.Sprites[_direction];

            _collider.offset = new Vector2(_collider.offset.y, _collider.offset.x);
            _collider.size = new Vector2(_collider.size.y, _collider.size.x);
        }
    }
}
