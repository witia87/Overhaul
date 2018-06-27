using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms
{
    public class RoomEntrance : MonoBehaviour
    {
        private void Awake()
        {
        }

        public void Update()
        {
        }

        public void OnDrawGizmos()
        {
            var offset = new Vector3(0.2f, 0, 0.2f);
            var offset2 = new Vector3(-0.2f, 0, 0.2f);

            DrawArrow.ForDebug(transform.position - offset,
                2 * offset, Color.green, 0.1f, 0);
            DrawArrow.ForDebug(transform.position - offset2,
                2 * offset2, Color.green, 0.1f, 0);
        }
    }
}