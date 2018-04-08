using UnityEngine;

namespace Assets.Modules
{
    public class Localizable : MonoBehaviour
    {
        public Vector3 Position
        {
            get { return transform.position; }
        }

        public Vector3 Direction
        {
            get { return transform.forward; }
        }

        protected virtual void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + transform.forward / 2,
                    transform.position + 3 * transform.forward);
            }
        }
    }
}