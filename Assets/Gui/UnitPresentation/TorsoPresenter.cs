using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class TorsoPresenter : ModulePresenter
    {
        [SerializeField] private Animator _animator;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            base.Update();
           // _animator.SetFloat("Speed", GetVelocity());
        }

        private float GetVelocity()
        {
            return Mathf.Sign(Module.transform.InverseTransformDirection(Module.Rigidbody.velocity).z) *
                   Module.Rigidbody.velocity.magnitude;
        }
    }
}