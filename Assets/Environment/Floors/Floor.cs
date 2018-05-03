using UnityEngine;

namespace Assets.Environment.Floors
{
    public class Floor : MonoBehaviour
    {
        [SerializeField] private LayerMask _floorLayerMask;

        public LayerMask FloorLayerMask
        {
            get { return _floorLayerMask; }
        }
    }
}