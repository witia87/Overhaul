using Assets.Environment.Units;
using UnityEngine;

namespace Assets.Cognitions.Maps
{
    public class MapStore : MonoBehaviour, IMapStore
    {
        public static LayerMask EnvironmentLayerMask;

        [SerializeField] private LayerMask _environmentLayerMask;
        private MapFactory _mapFactory;

        public IMap GetMap(IUnit unit)
        {
            return _mapFactory.GetMap(unitn);
        }

        private void Awake()
        {
            EnvironmentLayerMask = _environmentLayerMask;
            _mapFactory = FindObjectOfType<MapFactory>();
            _mapFactory.Initialize();
        }
    }
}