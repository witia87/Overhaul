using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns
{
    public class Gun : MonoBehaviour, IGun
    {
        public int AmmoLeftInTheClip
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int ClipSize
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int TotalAmmoLeft
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Fire()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }        
    }
}
