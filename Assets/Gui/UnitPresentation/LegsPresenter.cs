using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class LegsPresenter : ModulePresenter
    {
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

        protected void Update()
        {
            base.Update();
            UpdatePosition();
            Animator.SetFloat("Speed", GetVelocity());
            Animator.SetBool("Stable", false);
        }

        private float GetVelocity()
        {
            return Mathf.Sign(Module.transform.InverseTransformDirection(Module.Rigidbody.velocity).z) *
                   Module.Rigidbody.velocity.magnitude;
        }
    }
}