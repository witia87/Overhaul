using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Modules.Turrets.Guns.Bullets
{
    public class BulletComponentFactory : IBulletComponentFactory
    {
        private readonly Vector3 _initialLocalDirection;
        private readonly Vector3 _initialLocalPosition;
        private readonly GameObject _parentGameObject;

        public BulletComponentFactory([NotNull] GameObject parentGameObject, Vector3 initialLocalPosition,
            Vector3 initialLocalDirection)
        {
            if (parentGameObject == null) throw new ArgumentNullException("ParentGameObject cannot be null.");
            _parentGameObject = parentGameObject;
            _initialLocalPosition = initialLocalPosition;
            _initialLocalDirection = initialLocalDirection;
        }


        public IBulletComponent Create()
        {
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            gameObject.transform.position = _parentGameObject.transform.TransformPoint(_initialLocalPosition);
            gameObject.transform.forward = _parentGameObject.transform.TransformDirection(_initialLocalDirection);
            var bullet = gameObject.AddComponent<BulletComponent>();
            return bullet;
        }
    }
}