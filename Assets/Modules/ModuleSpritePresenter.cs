using System;
using UnityEngine;

namespace Assets.Modules
{
    public class ModuleSpritePresenter : MonoBehaviour
    {
        public Module Module;
        protected Animator Animator;
        protected SpriteRenderer SpriteRenderer;
        protected Vector2 SpriteSize;

        protected Vector3 BaseCameraEulerAngles;
        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteSize = SpriteRenderer.sprite.rect.size;

            BaseCameraEulerAngles = GameMechanics.Stores.CameraStore.CameraEulerAngles;

            offset = Quaternion.Euler(BaseCameraEulerAngles)*Vector3.back;
            offset *= (Module.Size.y/2)*(Mathf.Sqrt(3)/4);

            _widthOffset = -Module.Size.x/ (2 * Mathf.Sqrt(2));
            _heightOffset = Module.Size.y / 4;
            _lengthOffset = -Module.Size.z / (2 * Mathf.Sqrt(2));
        }

        Vector3 offset;
        private float _widthOffset;
        private float _heightOffset;
        private float _lengthOffset;
        protected virtual void Update()
        {
            gameObject.transform.eulerAngles = BaseCameraEulerAngles;
            gameObject.transform.position = new Vector3(
                Module.gameObject.transform.position.x + offset.x,
                Module.gameObject.transform.position.y + offset.y,
                Module.gameObject.transform.position.z + offset.z
                );
        }
    }
}