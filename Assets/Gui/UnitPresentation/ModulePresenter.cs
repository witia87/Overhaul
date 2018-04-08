using Assets.Gui.Cameras;
using Assets.Modules.Units.Bodies;
using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class ModulePresenter : MonoBehaviour
    {
        [SerializeField] protected int _angleDivisionCount = 8;
        private float _angleStep;


        private int _lastAngleX;
        private int _lastAngleY;
        private int _lastAngleZ;
        protected CameraStore CameraStore;
        [SerializeField] protected BodyModule Module;

        public bool HaveAnglesChanged { get; private set; }

        protected virtual void Awake()
        {
            CameraStore = FindObjectOfType<CameraStore>();
            _angleStep = 360.0f / _angleDivisionCount;
        }

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
            _lastAngleZ = newAnglesZ;
        }

        public void RefreshAngles()
        {
            transform.eulerAngles = new Vector3(_lastAngleX * _angleStep, _lastAngleY * _angleStep,
                _lastAngleZ * _angleStep);
        }
    }
}