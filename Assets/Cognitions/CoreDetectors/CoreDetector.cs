using Assets.Cores;
using Assets.Modules.Vision;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.CoreDetectors
{
    public class CoreDetector : DiamondDetector
    {
        private readonly int _layerMask = Layers.Map | Layers.Environment | Layers.Organism | Layers.Structure;

        public Core TargetCore;

        public void Update()
        {
            foreach (var collidingObject in CollidingGameObjects)
            {
                var core = collidingObject.GetComponent<Core>();
                if (core != null)
                {
                    var ray = collidingObject.transform.position - gameObject.transform.position;
                    RaycastHit hit;
                    if (Physics.Raycast(gameObject.transform.position, ray, out hit, Length, _layerMask)
                        && hit.transform.position == collidingObject.transform.position)
                    {
                        TargetCore = core;
                        return;
                    }
                }
            }
            TargetCore = null;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Core>() != null)
            {
                CollidingGameObjects.Add(other.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            if (TargetCore != null)
            {
                Debug.DrawLine(gameObject.transform.position + Vector3.up,
                    TargetCore.gameObject.transform.position + Vector3.up,
                    Color.red);
            }
        }

        public void OnGUI()
        {
            if (TargetCore != null)
            {
                var style = new GUIStyle();
                var rectWidth = Screen.width/4;
                var rectHeight = Screen.height/50;
                var rect = new Rect(Screen.width - rectWidth, Screen.height - rectHeight, rectWidth, rectHeight);
                style.alignment = TextAnchor.LowerRight;
                style.fontSize = rectHeight;
                style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
                var text = "Press E to connect to the: " + TargetCore.gameObject.name;
                GUI.Label(rect, text, style);

                Debug.DrawLine(gameObject.transform.position + Vector3.up,
                    TargetCore.gameObject.transform.position + Vector3.up,
                    Color.red);
            }
        }
    }
}