using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns.Bullets
{
    public class BulletComponentFactory : IBulletComponentFactory
    {
        private readonly Vector3 _initialLocalDirection;
        private readonly Vector3 _initialLocalPosition;
        private readonly GameObject _parrentGameObject;

        public BulletComponentFactory([NotNull] GameObject parrentGameObject, Vector3 initialLocalPosition,
            Vector3 initialLocalDirection)
        {
            if (parrentGameObject == null) throw new ArgumentNullException("ParrentGameObject cannot be null.");
            _parrentGameObject = parrentGameObject;
            _initialLocalPosition = initialLocalPosition;
            _initialLocalDirection = initialLocalDirection;
        }


        public IBulletComponent Create()
        {
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            gameObject.transform.position = _parrentGameObject.transform.TransformPoint(_initialLocalPosition);
            gameObject.transform.forward = _parrentGameObject.transform.TransformDirection(_initialLocalDirection);
            var bullet = gameObject.AddComponent<BulletComponent>();
            return bullet;
        }
    }
}