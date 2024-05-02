using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class RestaurantInfo
    {
        [SerializeField]
        private List<StageBuidUnitInfo> _buildUnits = new List<StageBuidUnitInfo>();
        public void AddBuildUnit(Vector3Int position, BuildUnit unit)
        {
            int sizeX = unit.Area.size.x;
            int sizeY = unit.Area.size.y;

            Vector3Int[] positions = new Vector3Int[sizeX * sizeY];
            int count = 0;

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    positions[count] = position + new Vector3Int(i, j, 0);
                    count++;
                }
            }

            _buildUnits.Add(
                new StageBuidUnitInfo
                {
                    Name = unit.Data.Name,
                    Type = unit.Data.Type,
                    Direction = unit.Direction,
                    Positions = positions,
                    Unit = unit
                }
            );
        }
        public void RemoveBuildUnit(Vector3Int position)
        {
            BuildUnit unit = GetBuildUnit(position);

            if (unit != null)
            {
                int count = unit.Area.size.x * unit.Area.size.y;

                for (int i = 0; i < count; i++)
                {
                    StageBuidUnitInfo furnituresData = _buildUnits.Find(data => data.Unit == unit);
                    _buildUnits.Remove(furnituresData);
                }
            }
        }
        public BuildUnit GetBuildUnit(Vector3Int position)
        {
            foreach (StageBuidUnitInfo e in _buildUnits)
            {
                int length = e.Positions.Length;
                for (int i = 0; i < length; i++)
                {
                    if (e.Positions[i] == position)
                    {
                        return e.Unit;
                    }
                }
            }
            return null;
        }
        public StageBuidUnitInfo[] GetChairs()
        {
            StageBuidUnitInfo[] chairs = _buildUnits.FindAll(data => data.Type == "Chair").ToArray();
            return chairs;
        }
        public StageBuidUnitInfo[] GetTables()
        {
            StageBuidUnitInfo[] tables = _buildUnits.FindAll(data => data.Type == "Table").ToArray();
            return tables;
        }
        public StageBuidUnitInfo GetTable(Vector3Int vector3Int)
        {
            StageBuidUnitInfo table = new StageBuidUnitInfo();
            StageBuidUnitInfo[] tables = _buildUnits.FindAll(data => data.Type == "Table").ToArray();

            for (int i = 0; i < tables.Length; i++)
            {
                for (int j = 0; j < tables[i].Positions.Length; j++)
                {
                    if (tables[i].Positions[j] == vector3Int)
                    {
                        table = tables[i];
                        break;
                    }
                }

                if (table.Unit != null) break;
            }

            return table;
        }
        public StageBuidUnitInfo[] GetCounters()
        {
            StageBuidUnitInfo[] counters = _buildUnits.FindAll(data => data.Type == "Counter").ToArray();
            return counters;
        }
        public StageBuidUnitInfo[] GetRefrigerators()
        {
            StageBuidUnitInfo[] refrigerators = _buildUnits.FindAll(data => data.Type == "Refrigerator").ToArray();
            return refrigerators;
        }
        public StageBuidUnitInfo[] GetCookTops()
        {
            StageBuidUnitInfo[] cooktops = _buildUnits.FindAll(data => data.Type == "Cooktop").ToArray();
            return cooktops;
        }
    }
}
