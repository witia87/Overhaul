using UnityEngine;

namespace Assets.Modules
{
    public class ModuleSpritePresenter : MonoBehaviour
    {
        private Vector3 _offset;
        protected Animator Animator;

        protected Vector3 BaseCameraEulerAngles;
        public Module Module;
        protected SpriteRenderer SpriteRenderer;
        protected Vector2 SpriteSize;

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteSize = SpriteRenderer.sprite.rect.size;

            BaseCameraEulerAngles = GameMechanics.Stores.CameraStore.CameraEulerAngles;

            _offset = Quaternion.Euler(BaseCameraEulerAngles)*Vector3.back;
            _offset *= Module.Size.y/2*(Mathf.Sqrt(3)/4);
            
            var baseSize = (SpriteSize.y/2)/SpriteRenderer.sprite.pixelsPerUnit;
            gameObject.transform.localScale = new Vector3(
                Module.Size.y * Mathf.Sqrt(3) / 2 / baseSize,
                Module.Size.y * Mathf.Sqrt(3) / 2 / baseSize,
                Module.Size.y * Mathf.Sqrt(3) / 2 / baseSize
                );
        }


        protected virtual void Update()
        {
            gameObject.transform.eulerAngles = BaseCameraEulerAngles;
            gameObject.transform.position = new Vector3(
                Module.gameObject.transform.position.x + _offset.x,
                Module.gameObject.transform.position.y + _offset.y,
                Module.gameObject.transform.position.z + _offset.z
                );
        }
    }
}