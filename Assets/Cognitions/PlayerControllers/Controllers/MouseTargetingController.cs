using System;
using Assets.Modules;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.PlayerControllers.Controllers
{
    public class MouseTargetingController : ITargetingController
    {
        private readonly int _floorLayerMask = Layers.Floor;

        private readonly int _targetLayerMask = Layers.Map | Layers.MapTransparent
                                                | Layers.Structure | Layers.StructureTransparent
                                                | Layers.Environment | Layers.EnvironmentTransparent
                                                | Layers.Organism | Layers.OrganismTransparent;

        private readonly UnityEngine.Camera _camera;

        public MouseTargetingController(UnityEngine.Camera camera)
        {
            _camera = camera;
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
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit, float.PositiveInfinity, _floorLayerMask))
            {
                TargetedPosition = mouseHit.point;
            }
            else
            {
                var a = 0;
            }

            if (Physics.Raycast(ray, out mouseHit, _targetLayerMask))
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

            IsFirePressed = Input.GetButton("Fire1");
        }

        public void OnDrawGizmos()
        {
            Debug.DrawLine(TargetedPosition,
                TargetedPosition + Vector3.up,
                Color.red);
        }
    }
}