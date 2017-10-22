using Assets.Gui.Cameras;
using Assets.Units;
using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class UnitPresenter : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        protected CameraStore CameraStore;

        protected virtual void Awake()
        {
            CameraStore = FindObjectOfType<CameraStore>();
        }

        protected virtual void Update()
        {
            transform.position = CameraStore.Pixelation.GetClosestPixelatedPosition(_unit.Center);
        }
    }
}