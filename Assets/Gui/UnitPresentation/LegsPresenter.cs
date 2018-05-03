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

        protected virtual void Update()
        {
            base.Update();
            Animator.SetFloat("Speed", GetVelocity());
        }

        private float GetVelocity()
        {
            return Mathf.Sign(Module.transform.InverseTransformDirection(Module.Rigidbody.velocity).z) *
                   Module.Rigidbody.velocity.magnitude;
        }
    }
}