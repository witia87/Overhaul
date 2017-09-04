using Assets.Gui.Cameras;
using Assets.Modules;
using UnityEngine;

namespace Assets.NewUnits
{
    public class MouseTargetingController2: MonoBehaviour
    {
        private ICameraStore _cameraStore;
        public Unit NewUnit;

        public Vector3 TargetedPosition { get; private set; }

        public bool IsTargetPresent
        {
            get { return TargetedModule != null; }
        }

        public Module TargetedModule { get; private set; }
        public bool IsFirePressed { get; private set; }

        public void Start()
        {
            _cameraStore = FindObjectOfType<CameraComponent>() as ICameraStore;
        }

        public void Update()
        {
            RaycastHit mouseHit;
            if (_cameraStore.Raycasts.ScreenPointToFloorRay(Input.mousePosition, out mouseHit))
            {
                var position = mouseHit.point;
                position.y = 0;
                NewUnit.LookAt(position);
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