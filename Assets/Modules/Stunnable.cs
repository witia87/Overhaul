using UnityEngine;

namespace Assets.Modules
{
    public class Stunnable : Localizable
    {
        public float StunResistanceTime = 10;
        protected float StunTimeLeft { get; private set; }

        protected float StunModifier
        {
            get { return Mathf.Max(0, 1 - StunTimeLeft / StunResistanceTime); }
        }

        public virtual void Stun(float time)
        {
            StunTimeLeft += time;
        }

        protected virtual void FixedUpdate()
        {
            StunTimeLeft = Mathf.Max(0, StunTimeLeft - Time.fixedDeltaTime);
        }
    }
}