using Assets.Gui;
using UnityEngine;

namespace Assets.Modules
{
    public class ModuleSpritePresenter<TParameters> : Presenter
    {
        private Vector3 _offset;
        private float _projectionHeight;

        private Module _module;

        protected SpriteRenderer SpriteRenderer;
        protected Vector2 SpriteSize;
        protected Animator Animator;
        protected TParameters Module;

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteSize = SpriteRenderer.sprite.rect.size;
            Module = transform.parent.GetComponent<TParameters>();

            var alpha = Mathf.Deg2Rad*CameraStore.CameraEulerAngles.x;

            _module = transform.parent.GetComponent<Module>();
            var moduleSize = Mathf.Max(_module.Size.x, _module.Size.y, _module.Size.z);

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
                CameraStore.Pixelation.GetClosestPixelatedPosition(_module.transform.position);
            gameObject.transform.position = gameObject.transform.position + _offset;
        }

        public virtual void Update()
        {
            gameObject.transform.eulerAngles = CameraStore.CameraEulerAngles;

            if (_module.IsConntectedToUnit)
            {
                var pixelatedOffset = CameraStore.Pixelation.GetPixelatedOffset(_module.Unit.transform.position,
                    _module.transform.position);
                gameObject.transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(_module.Unit.transform.position) +
                    pixelatedOffset + _offset;
            }
            else
            {
                gameObject.transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(_module.transform.position) + _offset;
            }
        }
    }
}