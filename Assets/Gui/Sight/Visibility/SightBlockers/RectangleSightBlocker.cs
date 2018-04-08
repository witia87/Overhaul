using UnityEngine;

namespace Assets.Gui.Sight.Visibility.SightBlockers
{
    public class RectangleSightBlocker : SightBlocker
    {
        protected override void Awake()
        {
            base.Awake();
            var polygon = new Vector2[4]
            {
                GetVector(transform.position +
                          new Vector3(-transform.localScale.x / 2, 0, -transform.localScale.z / 2)),
                GetVector(transform.position +
                          new Vector3(-transform.localScale.x / 2, 0, transform.localScale.z / 2)),
                GetVector(transform.position +
                          new Vector3(transform.localScale.x / 2, 0, transform.localScale.z / 2)),
                GetVector(transform.position +
                          new Vector3(transform.localScale.x / 2, 0, -transform.localScale.z / 2))
            };
            SightStore.RegisterWallRectangle(polygon);
        }

        private Vector2 GetVector(Vector3 position)
        {
            return new Vector2(position.x, position.z);
        }
    }
}