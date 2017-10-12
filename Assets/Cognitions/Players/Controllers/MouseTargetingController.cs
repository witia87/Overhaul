using Assets.Gui;
using Assets.Gui.Cameras;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Cognitions.Players.Controllers
{
    public class MouseTargetingController : ITargetingController
    {
        private readonly BoardStore _guiStore;

        public MouseTargetingController(BoardStore guiStore)
        {
            _guiStore = guiStore;
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
            /*RaycastHit mouseHit;
            if (_guiStore.Raycasts.ScreenPointToModuleRay(Input.mousePosition, out mouseHit))
            {
                var module = mouseHit.transform.gameObject.GetComponent<Module>();
                if (mouseHit.transform.gameObject.GetComponent<Module>() != null)
                {
                    TargetedPosition = mouseHit.point;
                    TargetedModule = module;
                }
            }
            else if (_guiStore.Raycasts.ScreenPointToEnvironmentRay(Input.mousePosition, out mouseHit))
            {
                TargetedPosition = mouseHit.point;
                TargetedModule = null;
            }
            else if (_guiStore.Raycasts.ScreenPointToEmptyTargetingRay(Input.mousePosition, out mouseHit))
            {
                TargetedPosition = mouseHit.point;
                TargetedModule = null;
            }

            IsFirePressed = Input.GetButton("Fire Gun");*/
        }

        public void OnDrawGizmos()
        {
            Debug.DrawLine(TargetedPosition,
                TargetedPosition + Vector3.up,
                Color.red);
        }
    }
}