using Assets.Gui;
using UnityEngine;

namespace Assets.NewUnits
{
    public class NewModuleSpritePresenter<TParameters> : Presenter
    {
        private Module _module;
        private Vector3 _offset;

        private int _prevWidth;
        private float _projectionHeight;
        protected Animator Animator;
        protected TParameters Module;

        protected SpriteRenderer SpriteRenderer;
        protected Vector2 SpriteSize;

        protected virtual void Start()
        {
            //var outliningObject = CreateOutliningObject();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.sortingLayerName = "Units";
            SpriteRenderer.sortingOrder = 2;

            SpriteRenderer.sortingLayerName = "Units";
            SpriteRenderer.material.SetInt("_OutlineSize", 1);

            SpriteSize = SpriteRenderer.sprite.rect.size;
            Module = transform.parent.GetComponent<TParameters>();

            var alpha = Mathf.Deg2Rad * CameraStore.CameraEulerAngles.x;

            _module = transform.parent.GetComponent<Module>();
            var moduleSize = Mathf.Max(_module.Size.x, _module.Size.y, _module.Size.z);

            var spriteHeight = SpriteSize.y / SpriteRenderer.sprite.pixelsPerUnit;
            _projectionHeight = moduleSize * Mathf.Cos(alpha);

            _offset = CameraStore.TransformVectorToCameraSpace(Vector3.back);
            _offset *= moduleSize * Mathf.Sin(alpha) / 2;

            transform.localScale = new Vector3(
                _projectionHeight / (spriteHeight / 2),
                _projectionHeight / (spriteHeight / 2),
                _projectionHeight / (spriteHeight / 2)
            );


            transform.position =
                CameraStore.Pixelation.GetClosestPixelatedPosition(_module.transform.position);
            transform.position = transform.position + _offset;
        }

        public Module RelativeModule = null;
        public virtual void Update()
        {
            transform.eulerAngles = CameraStore.CameraEulerAngles;

            if (RelativeModule != null)
            {
                var pixelatedOffset = CameraStore.Pixelation.GetPixelatedOffset(RelativeModule.transform.position,
                    _module.transform.position);
                transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(RelativeModule.transform.position) +
                    pixelatedOffset + _offset;
            }
            else
            {
                transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(_module.transform.position) + _offset;
            }
        }

        private void LateUpdate()
        {
            SpriteRenderer.sprite = SpriteRenderer.sprite;
            if (_prevWidth != SpriteRenderer.sprite.texture.width)
            {
                _prevWidth = SpriteRenderer.sprite.texture.width;
                SpriteRenderer.material.SetInt("_TexWidth", SpriteRenderer.sprite.texture.width);
                SpriteRenderer.material.SetInt("_TexHeight", SpriteRenderer.sprite.texture.height);
            }
        }
    }
}