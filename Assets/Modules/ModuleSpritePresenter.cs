using Assets.Gui;
using UnityEngine;

namespace Assets.Modules
{
    public class ModuleSpritePresenter : Presenter
    {
        private Vector3 _offset;
        private float _projectionHeight;
        protected Animator Animator;

        public Module Module;

        protected SpriteRenderer SpriteRenderer;
        protected Vector2 SpriteSize;

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteSize = SpriteRenderer.sprite.rect.size;

            var alpha = Mathf.Deg2Rad*CameraStore.CameraEulerAngles.x;

            var moduleSize = Mathf.Max(Module.Size.x, Module.Size.y, Module.Size.z);

            var spriteHeight = SpriteSize.y/SpriteRenderer.sprite.pixelsPerUnit;
            _projectionHeight = moduleSize*Mathf.Cos(alpha);

            _offset = CameraStore.TransformVectorToCameraSpace(Vector3.back);
            _offset *= moduleSize*Mathf.Sin(alpha)/2;

            gameObject.transform.localScale = new Vector3(
                _projectionHeight/(spriteHeight/2),
                _projectionHeight/(spriteHeight/2),
                _projectionHeight/(spriteHeight/2)
                );


            gameObject.transform.position =
                CameraStore.Pixelation.GetClosestPixelatedPosition(Module.transform.position);
            gameObject.transform.position = gameObject.transform.position + _offset;
        }

        public virtual void Update()
        {
            gameObject.transform.eulerAngles = CameraStore.CameraEulerAngles;

            if (Module.IsConntectedToUnit)
            {
                var pixelatedOffset = CameraStore.Pixelation.GetPixelatedOffset(Module.Unit.transform.position,
                    Module.transform.position);
                gameObject.transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(Module.Unit.transform.position) +
                    pixelatedOffset + _offset;
            }
            else
            {
                gameObject.transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(Module.transform.position) + _offset;
            }
        }
    }
}