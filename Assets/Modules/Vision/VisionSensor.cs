using System.Collections.Generic;
using Assets.Cores;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Modules.Vision
{
    public class VisionSensor : DiamondDetector, IVisionSensor
    {
        private readonly int _layerMask = Layers.Map | Layers.Environment | Layers.Organism | Layers.Structure |
                                          Layers.Floor;

        [Range(0.1f, Mathf.PI/2)] public float HorizontalAngleTolerance = Mathf.PI/3;
        public string TagToDetect = "Player";

        public GameObject TargetedObject;

        [Range(0.1f, Mathf.PI/2)] public float VerticalAngleTolerance = Mathf.PI/4;

        [Range(0.1f, 100)] public float VisionLenght = 10;

        public Vector3 SightPosition
        {
            get { return gameObject.transform.position; }
        }

        public List<GameObject> VisibleGameObjects { get; private set; }
        public List<Core> VisibleCores { get; private set; }

        protected override void Awake()
        {
            Length = VisionLenght;
            Width = Mathf.Tan(HorizontalAngleTolerance)*VisionLenght;
            Height = Mathf.Tan(VerticalAngleTolerance)*VisionLenght;
            VisibleGameObjects = new List<GameObject>();
            VisibleCores = new List<Core>();
            base.Awake();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagToDetect)
            {
                CollidingGameObjects.Add(other.gameObject);
            }
        }

        public void Update()
        {
            VisibleGameObjects = new List<GameObject>();
            VisibleCores = new List<Core>();
            foreach (var collidingObject in CollidingGameObjects)
            {
                if (collidingObject == null)
                {
                    var a = 0;
                }
                TargetedObject = collidingObject;
                var ray = collidingObject.transform.position + Vector3.up/2 - gameObject.transform.position;
                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, ray, out hit, _layerMask)
                    && hit.transform.gameObject == collidingObject)
                {
                    VisibleGameObjects.Add(collidingObject);
                    var core = collidingObject.GetComponent<Core>();
                    if (core != null)
                    {
                        VisibleCores.Add(core);
                    }

                    DrawArrow.ForDebug(gameObject.transform.position,
                        ray, Color.green, 0.1f, 0);
                }
                else
                {
                    DrawArrow.ForDebug(gameObject.transform.position,
                        ray, Color.red, 0.1f, 0);
                }
            }
        }

        public void OnGui()
        {
            int w = Screen.width, h = Screen.height;
            var style = new GUIStyle();

            var rect = new Rect(0, 0, w, h*2/100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h*2/100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            var text = "Visible objects count: " + VisibleGameObjects.Count;
            GUI.Label(rect, text, style);
        }
    }
}