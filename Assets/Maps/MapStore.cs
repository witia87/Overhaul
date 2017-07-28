using System.Collections.Generic;
using Assets.Maps.Dangers;
using Assets.Maps.Nodes;
using UnityEngine;

namespace Assets.Maps
{
    public class MapStore : MonoBehaviour, IMapStore
    {
        [SerializeField] private int _gridWidth = 40;
        [SerializeField] private int _gridLength = 40;
        [SerializeField] private float _baseGridUnitSize = 1;
        [SerializeField] private int _scalesCount= 6;
        [SerializeField] private LayerMask _floorDetectionLayerMask;

        public float BaseGridUnitSize { get { return _baseGridUnitSize; } }

        public float MapWidth
        {
            get { return _gridWidth * _baseGridUnitSize; }
        }

        public float MapLength
        {
            get { return _gridLength * _baseGridUnitSize; }
        }

        private List<INode[,]> _higherScaleGrids = new List<INode[,]>();
        private BaseNode[,] _baseGrid;

        private DangerStore _dangers;
        public IDangerStore Dangers { get { return _dangers; } }

        public IMap GetMap(int scale, FractionId fractionId)
        {
            if (scale == 0)
            {
                return new Map(_baseGrid, 0);
            }
            else
            {
                return new Map(_higherScaleGrids[scale -1], scale);
            }
        }

        private void Update()
        {
            _dangers.Update();
        }
        
        private void Awake()
        {
            var mapInitializer = new MapInitializer(_gridWidth, _gridLength, _floorDetectionLayerMask);
            _baseGrid = mapInitializer.InitializeBaseGrid();
            for (int i = 1; i < _scalesCount; i++)
            {
                _higherScaleGrids.Add(mapInitializer.InitializeHigherScaleGrid(_baseGrid, i));
            }

            _dangers = new DangerStore(_baseGrid);
        }

        void OnDrawGizmos()
        {
            _dangers.OnDrawGizmos();
        }
    }
}