using UnityEngine;

namespace Assets.Modules
{
    public class Unit : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
        }
    }
}