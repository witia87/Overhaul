using Assets.Modules.Targeting.Guns.Bullets;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Assets.Modules.Targeting.Guns
{
    public class GunModule : Module, IGunControl
    {
        private BulletsFactory[] _bulletFactories;

        [SerializeField] private int _clipSize = 30;

        [SerializeField] private float _cooldownTime = 0.5f;

        [SerializeField] private Vector2 _efectiveRange = new Vector2(5, 10);

        [SerializeField] private float _reloadTime = 2f;

        [SerializeField] private int _totalAmmoLeft = 90;

        private float _verticalDirectionCorection;

        public bool IsDangerous;

        public float ReloadTimeLeft { get; private set; }

        public Vector2 EfectiveRange
        {
            get { return _efectiveRange; }
        }

        public void SetFire(bool isSetToFire)
        {
            _verticalDirectionCorection = 0;
            IsFiring = isSetToFire;
        }

        public void SetFire(float distance, float bulletHeightAtAGivenDisntance)
        {
            _verticalDirectionCorection = new Vector3(0, bulletHeightAtAGivenDisntance - FirePosition.y, distance)
                .normalized.y;
            IsFiring = true;
        }

        public bool IsFiring { get; private set; }

        public int AmmoLeftInTheClip { get; private set; }

        public int ClipSize
        {
            get { return _clipSize; }
        }

        public Vector3 FirePosition
        {
            get { return _bulletFactories[_bulletFactories.Length / 2].transform.position; }
        }

        public Vector3 FireDirection
        {
            get { return _bulletFactories[_bulletFactories.Length / 2].transform.forward; }
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

        public void Reload()
        {
            if (TotalAmmoLeft > 0)
            {
                IsFiring = false;
                ReloadTimeLeft = _reloadTime;
            }
        }

        protected override void Awake()
        {
            base.Awake();
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
                    bulletsFactory.Create(_verticalDirectionCorection);
                }
                if (IsDangerous)
                {
                    MapStore.Dangers.RegisterLineOfFire(transform.position, transform.forward, 0.5f);
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