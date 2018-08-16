using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms.Covers
{
    public class CoverInitializer : MonoBehaviour
    {
        public Cover Initialize()
        {
            return new Cover(transform.position, transform.localScale.x, transform.localScale.z);
        }
    }
}