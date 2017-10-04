using UnityEngine;

namespace Assets.Sight.SightBlockers
{
    public class RectangleSightBlocker: SightBlocker
    {
        protected BoxCollider BoxCollider;
        protected override void Awake()
        {
            base.Awake();
            BoxCollider = GetComponent<BoxCollider>();
        }

        protected void Start()
        {
            var polygon = new Vector2[4]
            {
                new Vector2(transform.position.x - transform.localScale.x/2,
                    transform.position.z - transform.localScale.z/2),
                new Vector2(transform.position.x - transform.localScale.x/2,
                    transform.position.z + transform.localScale.z/2),
                new Vector2(transform.position.x + transform.localScale.x/2,
                    transform.position.z + transform.localScale.z/2),
                new Vector2(transform.position.x + transform.localScale.x/2,
                    transform.position.z - transform.localScale.z/2)
            };
            SightStore.RegisterPolygon(polygon);
        }
    }
}