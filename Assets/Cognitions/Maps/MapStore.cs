using Assets.Cognitions.Maps.Dangers;
using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Cognitions.Maps
{
    public class MapStore : MonoBehaviour, IMapStore
    {
        public static LayerMask EnvironmentLayerMask;

        private DangerStore _dangers;
        [SerializeField] private LayerMask _environmentLayerMask;
        private MapFactory _mapFactory;

        private void Update()
        {
            _dangers.Update();
        }

        private void Awake()
        {
            EnvironmentLayerMask = _environmentLayerMask;
            _mapFactory = FindObjectOfType<MapFactory>();
            _mapFactory.Initialize();
        }

        private void OnDrawGizmos()
        {
            if (_dangers != null)
            {
                _dangers.OnDrawGizmos();
            }
        }

        public IMap GetMap(IUnit unit)
        {
            return _mapFactory.GetMap(unit.UnitScale, unit.Fraction);
        }
    }
}