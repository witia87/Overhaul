using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class LegsPresenter : ModulePresenter
    {
        [SerializeField] private float _stabilityAngle = 30;
        protected Animator Animator;

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponent<Animator>();
        }

        public void UpdatePosition()
        {
            transform.position =
                CameraStore.Pixelation.GetClosestPixelatedPosition(Module.Top);
        }

        protected override void Update()
        {
            base.Update();
            UpdatePosition();
            Animator.SetFloat("Speed", GetVelocity());
            Animator.SetBool("Stable", GetStable());
        }

        private float GetVelocity()
        {
            return Mathf.Sign(Module.transform.InverseTransformDirection(Module.Rigidbody.velocity).z) *
                   Module.Rigidbody.velocity.magnitude;
        }

        private bool GetStable()
        {
            return Vector3.Angle(Module.transform.up, Vector3.up) < _stabilityAngle;
        }
    }
}