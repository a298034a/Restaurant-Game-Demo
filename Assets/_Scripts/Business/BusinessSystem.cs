using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Restaurant
{
    public class BusinessSystem : MonoBehaviour
    {
        public bool HasSeat { get { return _seats.Count > 0 ? true : false; } }
        public bool HasOreder { get { return _orderInfos.Count > 0 ? true : false; } }
        public Transform Entrance => _entrace;
        public Transform ChefWaitingArea => _chefWaitingArea;
        public Transform WaiterWaitingArea => _waiterWaitingArea;

        [SerializeField]
        private Transform _entrace;

        [SerializeField]
        private Transform _chefWaitingArea;

        [SerializeField]
        private Transform _waiterWaitingArea;

        private List<Customer> _customers = new List<Customer>();

        [SerializeField]
        private List<OrderInfo> _orderInfos = new List<OrderInfo>();

        private Queue<Seat> _seats = new Queue<Seat>();

        private DishesUnitSpawner _dishesUnitSpawner;
        private CustomerSpawner _customerSpawner;

        public void Init(DishesUnitSpawner dishesUnitSpawner, CustomerSpawner customerSpawner)
        {
            _dishesUnitSpawner = dishesUnitSpawner;
            _customerSpawner = customerSpawner;
        }
        public DishesUnit InstantiateDishes(string name, Vector3 position)
        {
            return _dishesUnitSpawner.Instantiate(name, position);
        }

        #region Order
        public void AddOrderInfo(OrderInfo info)
        {
            _orderInfos.Add(info);
        }
        public void RemoveOrderInfo(OrderInfo info)
        {
            _orderInfos.Remove(info);
        }
        public OrderInfo GetOrderInfo(OrderInfo.OrderState orderState)
        {
            OrderInfo orderInfo = _orderInfos.Find(x => x.State == orderState);
            return orderInfo;
        }
        #endregion

        [Button]
        public void InstantiateCustomer(string name)
        {
            Customer customer = _customerSpawner.Instantiate(name, _entrace.position);
            _customers.Add(customer);
        }
        public void DemoStartBusiness()
        {
            StartCoroutine(InstantiateCustomerProcess());
        }
        IEnumerator InstantiateCustomerProcess()
        {
            int time = 0;
            while (time < 4)
            {
                time++;
                Customer customer = _customerSpawner.InstantiateRandom(_entrace.position);
                _customers.Add(customer);

                yield return new WaitForSeconds(10);
            }
        }

        #region Seat
        public Seat GetSeat()
        {
            return _seats.Dequeue();
        }
        public void AddSeat(Seat seat)
        {
            _seats.Enqueue(seat);
        }
        public void CalculateSeats()
        {
            _seats.Clear();

            Vector3Int[] chairComparePos = _GetChairComparePos();
            List<Vector3Int> tablePos = _GetTablePos();

            StageBuidUnitInfo[] chairs = RestaurantInfoProvider.Ins.Info.GetChairs();

            int length = chairComparePos.Length;

            for (int i = 0; i < length; i++)
            {
                if (tablePos.Contains(chairComparePos[i]) && !_PosIsOccupied(chairComparePos[i]))
                {
                    _seats.Enqueue(new Seat
                    {
                        Position = chairs[i].Positions[0],
                        Transform = chairs[i].Unit.gameObject.transform,
                        Direction = chairs[i].Direction,
                        TableUnitInfo = RestaurantInfoProvider.Ins.Info.GetTable(chairComparePos[i])
                    });
                }
            }
        }
        private Vector3Int[] _GetChairComparePos()
        {
            StageBuidUnitInfo[] chairs = RestaurantInfoProvider.Ins.Info.GetChairs();

            int length = chairs.Length;
            Vector3Int[] tempPos = new Vector3Int[length];

            for (int i = 0; i < length; i++)
            {
                switch (chairs[i].Direction)
                {
                    case 0:
                        tempPos[i] = chairs[i].Positions[0] + new Vector3Int(0, -1, 0);
                        break;
                    case 1:
                        tempPos[i] = chairs[i].Positions[0] + new Vector3Int(-1, 0, 0);
                        break;
                    case 2:
                        tempPos[i] = chairs[i].Positions[0] + new Vector3Int(0, 1, 0);
                        break;
                    case 3:
                        tempPos[i] = chairs[i].Positions[0] + new Vector3Int(1, 0, 0);
                        break;
                }
            }

            return tempPos;
        }
        private List<Vector3Int> _GetTablePos()
        {
            StageBuidUnitInfo[] tables = RestaurantInfoProvider.Ins.Info.GetTables();

            int length = tables.Length;
            List<Vector3Int> tempPos = new List<Vector3Int>();

            for (int i = 0; i < length; i++)
            {
                int posLength = tables[i].Positions.Length;

                for (int j = 0; j < posLength; j++)
                {
                    tempPos.Add(tables[i].Positions[j]);
                }
            }

            return tempPos;
        }
        private bool _PosIsOccupied(Vector3Int pos)
        {
            foreach (Seat e in _seats)
            {
                if (e.Position == pos) return true;
            }
            return false;
        }
        #endregion

        #region Counter
        [SerializeField]
        private List<Counter> _counters = new List<Counter>();
        private Vector3 _tileCenterFix = new Vector3(0.5f, 0.5f, 0);
        public void CalculateCounters()
        {
            _counters.Clear();

            StageBuidUnitInfo[] counters = RestaurantInfoProvider.Ins.Info.GetCounters();

            int length = counters.Length;
            for (int i = 0; i < length; i++)
            {
                int posLength = counters[i].Positions.Length;
                Vector3 placingPosFix = Vector3.zero;
                if (counters[i].Direction == 0 || counters[i].Direction == 2)
                { placingPosFix = new Vector3(0, 0.5f, 0); }

                for (int j = 0; j < posLength; j++)
                {
                    //預設每個櫃台佔用的格子只有 2 個比較位置
                    Vector3Int[] comparePos = _GetCounterComparePos(counters[i].Positions[j], counters[i]);

                    //檢查位置是否已被其他物件佔用 IMPL: 超出地圖範圍
                    bool pos0 = RestaurantInfoProvider.Ins.Info.GetBuildUnit(comparePos[0]) == null;
                    bool pos1 = RestaurantInfoProvider.Ins.Info.GetBuildUnit(comparePos[1]) == null;

                    if (pos0 && pos1)
                    {
                        _counters.Add(new Counter()
                        {
                            Positions = new Vector3[2] { comparePos[0] + _tileCenterFix, comparePos[1] + _tileCenterFix },
                            PlacingPosition = counters[i].Positions[j] + _tileCenterFix + placingPosFix,
                            StageBuidUnitInfo = counters[i]
                        });
                    }
                    else if (pos0)
                    {
                        _counters.Add(new Counter()
                        {
                            Positions = new Vector3[1] { comparePos[0] + _tileCenterFix },
                            PlacingPosition = counters[i].Positions[j] + _tileCenterFix + placingPosFix,
                            StageBuidUnitInfo = counters[i]
                        });
                    }
                    else if (pos1)
                    {
                        _counters.Add(new Counter()
                        {
                            Positions = new Vector3[1] { comparePos[1] + _tileCenterFix },
                            PlacingPosition = counters[i].Positions[j] + _tileCenterFix + placingPosFix,
                            StageBuidUnitInfo = counters[i]
                        });
                    }
                }
            }
        }
        public Counter GetClosestCounter(Vector3 selfPos)
        {
            Counter closestCounter = null;
            float closestDistance = float.MaxValue;

            foreach (Counter counter in _counters)
            {
                if (!counter.Occupied)
                {
                    foreach (Vector3 position in counter.Positions)
                    {
                        float distance = Vector3.Distance(selfPos, position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestCounter = counter;
                        }
                    }
                }
            }

            return closestCounter;
        }
        /// <summary>
        /// 計算櫃台要比對的位置
        /// </summary>
        /// <param name="counters"></param>
        private Vector3Int[] _GetCounterComparePos(Vector3Int pos, StageBuidUnitInfo counter)
        {
            Vector3Int[] comparePos = new Vector3Int[2];

            switch (counter.Direction)
            {
                case 0://下
                case 2://上
                    comparePos[0] = pos + Vector3Int.down;
                    comparePos[1] = pos + Vector3Int.up;
                    break;

                case 1://左
                case 3://右
                    comparePos[0] = pos + Vector3Int.right;
                    comparePos[1] = pos + Vector3Int.left;
                    break;
            }

            return comparePos;
        }
        #endregion
    }
}
