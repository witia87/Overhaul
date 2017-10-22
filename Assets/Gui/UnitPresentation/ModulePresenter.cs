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

        private int _lastAngleX;
        private int _lastAngleY;
        private int _lastAngleZ;
        public void RecalculateAngles()
        {
            var newEulerAngles = Module.transform.eulerAngles;
            var newAnglesX = Mathf.RoundToInt(newEulerAngles.x / 45);
            var newAnglesY = Mathf.RoundToInt(newEulerAngles.y / 45);
            var newAnglesZ = Mathf.RoundToInt(newEulerAngles.z / 45);

            HaveAnglesChanged = newAnglesX != _lastAngleX ||
                                newAnglesY != _lastAngleY ||
                                newAnglesZ != _lastAngleZ;

            _lastAngleX = newAnglesX;
            _lastAngleY = newAnglesY;
            _lastAngleZ= newAnglesZ;
        }

        public bool HaveAnglesChanged{get;private set;}

        protected void RefreshAngles()
        {
            transform.eulerAngles = new Vector3(_lastAngleX * 45, _lastAngleY * 45, _lastAngleZ * 45);
        }
    }
}