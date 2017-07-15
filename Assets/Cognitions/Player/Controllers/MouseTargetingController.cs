using Assets.Gui.MainCamera;
using Assets.Modules;
using UnityEngine;

namespace Assets.Cognitions.Player.Controllers
{
    public class MouseTargetingController : ITargetingController
    {
        private readonly ICameraStore _cameraStore;

        public MouseTargetingController(ICameraStore cameraStore)
        {
            _cameraStore = cameraStore;
        }

        public Vector3 TargetedPosition { get; private set; }

        public bool IsTargetPresent
        {
            get { return TargetedModule != null; }
        }

        public Module TargetedModule { get; private set; }
        public bool IsFirePressed { get; private set; }

        public void Start()
        {
        }

        public void Update()
        {
            RaycastHit mouseHit;
            if (_cameraStore.Raycasts.ScreenPointToFloorRay(Input.mousePosition, out mouseHit))
            {
                TargetedPosition = mouseHit.point;
            }

            if (_cameraStore.Raycasts.ScreenPointToRay(Input.mousePosition, out mouseHit))
            {
                var module = mouseHit.transform.gameObject.GetComponent<Module>();
                if (mouseHit.transform.gameObject.GetComponent<Module>() != null)
                {
                    TargetedModule = module;
                }
            }
            else
            {
                TargetedModule = null;
            }

            IsFirePressed = Input.GetButton("Fire Gun");
        }

        public void OnDrawGizmos()
        {
            Debug.DrawLine(TargetedPosition,
                TargetedPosition + Vector3.up,
                Color.red);
        }
    }
}