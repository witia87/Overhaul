using Assets.Gui;
using UnityEngine;

namespace Assets.Modules
{
    public class ModuleSpritePresenter : Presenter
    {
        private Vector3 _offset;
        protected Animator Animator;

        protected Vector3 BaseCameraEulerAngles;

        public GameObject FixedOffsetGameObject;
        public Module Module;
        protected SpriteRenderer SpriteRenderer;
        protected Vector2 SpriteSize;

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteSize = SpriteRenderer.sprite.rect.size;

            BaseCameraEulerAngles = CameraStore.CameraEulerAngles;

            _offset = Quaternion.Euler(BaseCameraEulerAngles)*Vector3.back;
            _offset *= Module.Size.y/2*(Mathf.Sqrt(3)/4);

            var baseSize = SpriteSize.y/2/SpriteRenderer.sprite.pixelsPerUnit;
            gameObject.transform.localScale = new Vector3(
                Module.Size.y*Mathf.Sqrt(3)/2/baseSize,
                Module.Size.y*Mathf.Sqrt(3)/2/baseSize,
                Module.Size.y*Mathf.Sqrt(3)/2/baseSize
                );
        }

        protected virtual void Update()
        {
            gameObject.transform.eulerAngles = BaseCameraEulerAngles;

            if (FixedOffsetGameObject != null)
            {
                gameObject.transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(FixedOffsetGameObject.transform.position) +
                    CameraStore.Pixelation.GetPixelatedOffset(
                        FixedOffsetGameObject.transform.position,
                        Module.transform.position);
            }
            else
            {
                gameObject.transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(Module.transform.position);
            }
            gameObject.transform.position = gameObject.transform.position + _offset;
        }
    }
}