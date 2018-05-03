using System.Collections.Generic;
using Assets.Cognitions.Maps;
using Assets.Cognitions.Maps.Dangers;
using Assets.Cognitions.Maps.Nodes;
using Assets.Environment;
using Assets.Environment.Guns.Bullets;
using UnityEngine;

namespace Assets.Resources
{
    public class MapStore : MonoBehaviour, IMapStore
    {
        private BaseNode[,] _baseGrid;
        [SerializeField] private float _baseGridUnitSize = 1;

        private DangerStore _dangers;
        [SerializeField] private LayerMask _floorDetectionLayerMask;
        [SerializeField] private int _gridLength = 40;
        [SerializeField] private int _gridWidth = 40;

        private readonly List<INode[,]> _higherScaleGrids = new List<INode[,]>();
        [SerializeField] private int _scalesCount = 6;

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

            _dangers = new DangerStore(_baseGrid, FindObjectsOfType<BulletsFactory>());
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