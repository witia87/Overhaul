using Assets.Gui.Cameras;
using Assets.Units;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class TorsoPresenter : ModulePresenter
    {
        protected override void Update()
        {
            base.Update();
            //transform.position = CameraStore.Pixelation.GetClosestPixelatedPosition(Module.Bottom);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}