using Assets.Gui.Cameras;
using Assets.Units;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class ModulePresenter : MonoBehaviour
    {
        protected CameraStore CameraStore;
        [SerializeField] protected Module Module;

        protected virtual void Awake()
        {
            CameraStore = FindObjectOfType<CameraStore>();
        }

        protected virtual void Update()
        {
            var newEulerAngles = Module.transform.eulerAngles;
            newEulerAngles.x = Mathf.RoundToInt(newEulerAngles.x / 45) * 45;
            newEulerAngles.y = Mathf.RoundToInt(newEulerAngles.y / 45) * 45;
            newEulerAngles.z = Mathf.RoundToInt(newEulerAngles.z / 45) * 45;
            transform.eulerAngles = newEulerAngles;

        }
    }
}