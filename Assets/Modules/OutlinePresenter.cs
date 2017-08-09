using UnityEngine;

namespace Assets.Modules
{
    public class OutlinePresenter : MonoBehaviour
    {
        private SpriteRenderer _parentRenderer;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
            _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _parentRenderer.sprite;
            _spriteRenderer.material = Resources.Load("Materials/OutlineMaterial", typeof(Material)) as Material;
            _spriteRenderer.sortingLayerName = "Units";
            _spriteRenderer.sortingOrder = _parentRenderer.sortingOrder - 1;
            _spriteRenderer.material.SetInt("_OutlineSize", 1);
        }

        private void Update()
        {
        }

        private int _prevWidth;
        private void LateUpdate()
        {
            _spriteRenderer.sprite = _parentRenderer.sprite;
            if (_prevWidth != _spriteRenderer.sprite.texture.width)
            {
                _prevWidth = _spriteRenderer.sprite.texture.width;
                _spriteRenderer.material.SetInt("_TexWidth", _spriteRenderer.sprite.texture.width);
                _spriteRenderer.material.SetInt("_TexHeight", _spriteRenderer.sprite.texture.height);
            }
        }
    }
}