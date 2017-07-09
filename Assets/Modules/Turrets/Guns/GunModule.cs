using Assets.Modules.Turrets.Guns.Bullets;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns
{
    public class GunModule : Module, IGunControl
    {
        private BulletsFactory[] _bulletFactories;

        [SerializeField] private int _clipSize = 30;

        [SerializeField] private float _cooldownTime = 0.5f;

        [SerializeField] private float _reloadTime = 2f;


        [SerializeField] private int _totalAmmoLeft = 90;
        public float ReloadTimeLeft { get; private set; }
        public bool IsFiring { get; private set; }

        public int AmmoLeftInTheClip { get; private set; }

        public int ClipSize
        {
            get { return _clipSize; }
        }

        public int TotalAmmoLeft
        {
            get { return _totalAmmoLeft; }
        }

        public float CooldownTime
        {
            get { return _cooldownTime; }
        }

        public float CooldownTimeLeft { get; private set; }

        public float ReloadTime
        {
            get { return _reloadTime; }
        }

        public bool IsReloading
        {
            get { return ReloadTimeLeft > 0; }
        }

        public void Fire()
        {
            IsFiring = true;
        }

        public void StopFiring()
        {
            IsFiring = false;
        }

        public void Reload()
        {
            if (TotalAmmoLeft > 0)
            {
                IsFiring = false;
                ReloadTimeLeft = _reloadTime;
            }
        }

        private void Awake()
        {
            AmmoLeftInTheClip = ClipSize;
            _bulletFactories = GetComponentsInChildren<BulletsFactory>();
        }

        private void Update()
        {
            CooldownTimeLeft = Mathf.Max(0, CooldownTimeLeft - Time.deltaTime);
            if (IsConntectedToUnit && IsReloading)
            {
                ReloadTimeLeft = Mathf.Max(0, ReloadTimeLeft - Time.deltaTime);
                if (ReloadTimeLeft <= 0)
                {
                    AmmoLeftInTheClip = Mathf.Min(_totalAmmoLeft, _clipSize);
                    _totalAmmoLeft -= AmmoLeftInTheClip;
                }
            }
            if (!IsReloading && AmmoLeftInTheClip > 0 && IsFiring && CooldownTimeLeft <= 0)
            {
                _totalAmmoLeft--;
                _clipSize--;
                CooldownTimeLeft = CooldownTime;
                foreach (var bulletsFactory in _bulletFactories)
                {
                    bulletsFactory.Create();
                }
            }
        }

        public override void Unmount()
        {
            ReloadTimeLeft = 0;
            base.Unmount();
        }
    }
}