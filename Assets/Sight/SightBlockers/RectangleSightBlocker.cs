using UnityEngine;

namespace Assets.Sight.SightBlockers
{
    public class RectangleSightBlocker: SightBlocker
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected void Start()
        {
            var polygon = new Vector2[4]
            {
                GetVector(transform.position + new Vector3(-transform.localScale.x/2, 0, -transform.localScale.z/2)),
                GetVector(transform.position + new Vector3(-transform.localScale.x/2, 0, transform.localScale.z/2)),
                GetVector(transform.position + new Vector3(transform.localScale.x/2, 0, transform.localScale.z/2)),
                GetVector(transform.position + new Vector3(transform.localScale.x/2, 0, -transform.localScale.z/2)),
            };
            SightStore.RegisterPolygon(polygon);
        }

        private Vector2 GetVector(Vector3 position)
        {
            var v = CameraStore.Pixelation.GetClosestPixelatedPosition(position);
            return new Vector2(v.x, v.z);
        }
    }
}