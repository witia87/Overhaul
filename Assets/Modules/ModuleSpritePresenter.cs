
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
            var outliningObject = CreateOutliningObject();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.sortingLayerName = "Units";
            SpriteRenderer.sortingOrder = 2;
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

        public virtual void Update()
        {
            transform.eulerAngles = CameraStore.CameraEulerAngles;

            if (_module.IsConntectedToUnit)
            {
                var pixelatedOffset = CameraStore.Pixelation.GetPixelatedOffset(_module.Unit.Position,
                    _module.transform.position);
                transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(_module.Unit.Position) +
                    pixelatedOffset + _offset;
            }
            else
            {
                transform.position =
                    CameraStore.Pixelation.GetClosestPixelatedPosition(_module.transform.position) + _offset;
            }
        }


        private GameObject CreateOutliningObject()
        {
            var outliningObject = new GameObject("Outline");
            outliningObject.transform.parent = transform;
            outliningObject.transform.localPosition = Vector3.zero;
            outliningObject.transform.localEulerAngles = Vector3.zero;
            outliningObject.transform.localScale = new Vector3(1,1,1);
            outliningObject.AddComponent<OutlinePresenter>();

            return outliningObject;
        }
    }
}