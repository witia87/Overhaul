using System.Collections.Generic;
using Assets.Maps;
using Assets.Maps.Dangers;
using Assets.Maps.Nodes;
using Assets.Units;
using UnityEngine;

namespace Assets.Resources
{
    public class MapStore : MonoBehaviour, IMapStore
    {
        [SerializeField] private float _baseGridUnitSize = 1;
        [SerializeField] private int _gridLength = 40;
        [SerializeField] private int _gridWidth = 40;

        private readonly List<INode[,]> _higherScaleGrids = new List<INode[,]>();
        [SerializeField] private int _scalesCount = 6;
        private BaseNode[,] _baseGrid;

        private DangerStore _dangers;
        [SerializeField] private LayerMask _floorDetectionLayerMask;

        public float BaseGridUnitSize
        {
            get { return _baseGridUnitSize; }
        }

        public float MapWidth
        {
            get { return _gridWidth * _baseGridUnitSize; }
        }

        public float MapLength
        {
            get { return _gridLength * _baseGridUnitSize; }
        }

        public IDangerStore Dangers
        {
            get { return _dangers; }
        }

        public IMap GetMap(int scale, FractionId fractionId)
        {
            if (scale == 0)
            {
                return new Map(_baseGrid, 0);
            }
            return new Map(_higherScaleGrids[scale - 1], scale);
        }

        private void Update()
        {
            _dangers.Update();
        }

        private void Awake()
        {
            var mapInitializer = new MapInitializer(_gridWidth, _gridLength, _floorDetectionLayerMask);
            _baseGrid = mapInitializer.InitializeBaseGrid();
            for (var i = 1; i < _scalesCount; i++)
            {
                _higherScaleGrids.Add(mapInitializer.InitializeHigherScaleGrid(_baseGrid, i));
            }

            _dangers = new DangerStore(_baseGrid);
        }

        private void OnDrawGizmos()
        {
            if (_dangers != null)
            {
                _dangers.OnDrawGizmos();
            }
        }
    }
}