using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class LegsPresenter : ModulePresenter
    {
        [SerializeField] private  Animator _animator;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            base.Update();
            _animator.SetFloat("Speed", GetVelocity());
            _animator.SetFloat("Rotation", GetAngularVelocity());
        }

        private float GetVelocity()
        {
            return Mathf.Sign(Module.transform.InverseTransformDirection(Module.Rigidbody.velocity).z) *
                   Module.Rigidbody.velocity.magnitude;
        }


        private float GetAngularVelocity()
        {
            return Mathf.Sign(Module.transform.InverseTransformDirection(Module.Rigidbody.angularVelocity).y) *
                   Module.Rigidbody.angularVelocity.magnitude;
        }

        /*
        private float GetForward()
        {
            return Module.transform.forward;
        }
        */
    }
}