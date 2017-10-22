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

        [SerializeField] protected int _angleDivisionCount = 8;
        private float _angleStep;
        protected virtual void Awake()
        {
            CameraStore = FindObjectOfType<CameraStore>();
            _angleStep = 360.0f / _angleDivisionCount;
        }


        private int _lastAngleX;
        private int _lastAngleY;
        private int _lastAngleZ;
        public void RecalculateAngles()
        {
            var newEulerAngles = Module.transform.eulerAngles;
            var newAnglesX = Mathf.RoundToInt(newEulerAngles.x / _angleStep);
            var newAnglesY = Mathf.RoundToInt(newEulerAngles.y / _angleStep);
            var newAnglesZ = Mathf.RoundToInt(newEulerAngles.z / _angleStep);

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
            transform.eulerAngles = new Vector3(_lastAngleX * _angleStep, _lastAngleY * _angleStep, _lastAngleZ * _angleStep);
        }
    }
}