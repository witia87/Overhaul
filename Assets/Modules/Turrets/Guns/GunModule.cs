using System;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns
{
    public class GunModule : Module, IGunControl
    {
        private bool _isFiring;

        public int AmmoLeftInTheClip
        {
            get { throw new NotImplementedException(); }
        }

        public int ClipSize
        {
            get { throw new NotImplementedException(); }
        }

        public int TotalAmmoLeft
        {
            get { throw new NotImplementedException(); }
        }

        public float CooldownTime
        {
            get { throw new NotImplementedException(); }
        }

        public float CooldownTimeLeft
        {
            get { throw new NotImplementedException(); }
        }

        public void Fire()
        {
            _isFiring = true;
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

        public bool IsFiring()
        {
            return _isFiring;
        }


        public override void Mount(Module parrentGameObject, Vector3 localPosition)
        {
            base.Mount(parrentGameObject, localPosition);
            ParrentModule.Rigidbody.mass += Rigidbody.mass;
            //Rigidbody.detectCollisions = false;
        }

        public override void Unmount()
        {
            ParrentModule.Rigidbody.mass -= Rigidbody.mass;
            //Rigidbody.detectCollisions = true;
            base.Unmount();
        }

    }
}