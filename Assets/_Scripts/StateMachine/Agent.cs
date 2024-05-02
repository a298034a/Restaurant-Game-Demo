using Pathfinding;
using System.Collections;
using UnityEngine;

namespace Restaurant
{
    public class Agent : MonoBehaviour
    {
        public OrderInfo OrderInfo => _orderInfo;
        public Animator CharacterAnimator => _characterAnimator;
        public Animator EmoteAnimator => _emoteAnimator;

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private Animator _emoteAnimator;
        [SerializeField] private Seeker _seeker;
        [SerializeField] private int _speed;
        [SerializeField] private float _nextWaypointDistance;
        [SerializeField] protected OrderInfo _orderInfo;

        private Path _path;
        private int _currentWayPoint = 0;
        private bool _reachEndOfPath = false;
        private bool _timeUp = false;

        private Coroutine _TimeCounting;

        public void SetTarget(Transform transform)
        {
            _reachEndOfPath = false;
            _path = null;
            _seeker.StartPath(_rb.position, transform.position, _OnPathCpmplete);
        }
        public void SetTarget(Vector3Int position)
        {
            _reachEndOfPath = false;
            _path = null;
            _seeker.StartPath(_rb.position, position, _OnPathCpmplete);
        }
        public void SetTarget(Vector3 position)
        {
            _reachEndOfPath = false;
            _path = null;
            _seeker.StartPath(_rb.position, position, _OnPathCpmplete);
        }
        public bool ReachEndOfPath()
        {
            return _reachEndOfPath;
        }
        public void SetTimer(float seconds)
        {
            if (_TimeCounting != null) StopCoroutine(_TimeCounting);

            _timeUp = false;
            _TimeCounting = StartCoroutine(_Timer(seconds));
        }
        public bool TimeUp()
        {
            return _timeUp;
        }
        public void ResetOnStateChenge()
        {
            _reachEndOfPath = false;
            _path = null;
            _timeUp = false;
        }
        private void Update()
        {
            if (_path == null)
            { return; }

            if (_currentWayPoint >= _path.vectorPath.Count)
            {
                _reachEndOfPath = true;
                if (Vector2.Distance(_rb.position, _path.vectorPath[_path.vectorPath.Count - 1]) < 0.1f)
                {
                    _rb.velocity = Vector2.zero;
                }
                return;
            }
            else
            {
                _reachEndOfPath = false;
            }

            Vector2 direction = ((Vector2)_path.vectorPath[_currentWayPoint] - _rb.position).normalized;

            _rb.velocity = direction * _speed;
            _CheckDirection();
            _UpdateLayer();

            float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWayPoint]);

            if (distance < _nextWaypointDistance)
            {
                _currentWayPoint++;
            }
        }
        private void _OnPathCpmplete(Path path)
        {
            if (!path.error)
            {
                _path = path;
                _currentWayPoint = 0;
            }
        }
        private void _CheckDirection()
        {
            float x = _rb.velocity.x;
            float y = _rb.velocity.y;

            if (_AbsoluteValue(x) > _AbsoluteValue(y))
            {
                if (_rb.velocity.x > 0)
                {
                    _characterAnimator.Play(Constant.ANIMATE_WALK_RIGHT);
                    return;
                }
                else if (_rb.velocity.x < 0)
                {
                    _characterAnimator.Play(Constant.ANIMATE_WALK_LEFT);
                    return;
                }
            }

            if (_AbsoluteValue(y) > _AbsoluteValue(x))
            {
                if (_rb.velocity.y > 0)
                {
                    _characterAnimator.Play(Constant.ANIMATE_WALK_UP);
                    return;
                }
                else if (_rb.velocity.y < 0)
                {
                    _characterAnimator.Play(Constant.ANIMATE_WALK_DOWN);
                    return;
                }
            }
        }
        private float _AbsoluteValue(float value)
        {
            return (value < 0) ? -value : value;
        }
        private void _UpdateLayer()
        {
            int layer = Mathf.RoundToInt(transform.position.y);

            _spriteRenderer.sortingOrder = layer * -1;
        }
        private IEnumerator _Timer(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            _timeUp = true;
        }
        #region OrderInfo
        public void SetOrderInfo(OrderInfo info)
        {
            _orderInfo = info;
        }
        public void RemoveOrderInfo()
        {
            _orderInfo = null;
        }
        #endregion
    }
}