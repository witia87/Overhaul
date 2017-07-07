using Assets.Gui;
using UnityEngine;

namespace Assets.Modules
{
    public class ModuleSpritePresenter : Presenter
    {
        private Vector3 _offset;
        private Vector3 _pixelatedOffset;
        private float _projectionHeight;
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

            var alpha = Mathf.Deg2Rad*BaseCameraEulerAngles.x;

            var moduleSize = Mathf.Max(Module.Size.x, Module.Size.y, Module.Size.z);

            var spriteHeight = SpriteSize.y/SpriteRenderer.sprite.pixelsPerUnit;
            _projectionHeight = moduleSize*Mathf.Cos(alpha);

            _offset = CameraStore.TransformVectorToCameraSpace(Vector3.back);
                //Quaternion.Euler(BaseCameraEulerAngles)*Vector3.back;
            _offset *= moduleSize*Mathf.Sin(alpha)/2;


            var baseSize = SpriteSize.y/2/SpriteRenderer.sprite.pixelsPerUnit;
            gameObject.transform.localScale = new Vector3(
                _projectionHeight/(spriteHeight/2),
                _projectionHeight/(spriteHeight/2),
                _projectionHeight/(spriteHeight/2)
                );


            gameObject.transform.position =
                CameraStore.Pixelation.GetClosestPixelatedPosition(Module.transform.position);
            gameObject.transform.position = gameObject.transform.position + _offset;

            if (FixedOffsetGameObject != null)
            {
                _pixelatedOffset = gameObject.transform.position - FixedOffsetGameObject.transform.position;
            }
        }

        protected override void Update()
        {
            gameObject.transform.eulerAngles = BaseCameraEulerAngles;

            if (FixedOffsetGameObject != null)
            {
                _pixelatedOffset = CameraStore.Pixelation.GetPixelatedOffset(FixedOffsetGameObject.transform.position,
                    Module.transform.position);
                gameObject.transform.position = CameraStore.Pixelation.GetClosestPixelatedPosition(
                    FixedOffsetGameObject.transform.position)
                                                + _pixelatedOffset;
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