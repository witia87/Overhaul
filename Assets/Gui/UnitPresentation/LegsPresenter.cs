using Assets.Gui.Cameras;
using Assets.Units;
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class LegsPresenter : ModulePresenter
    {
        [SerializeField] protected TorsoPresenter TorsoPresenter;
        protected override void Update()
        {
            base.Update();
            var position = CameraStore.Pixelation.GetClosestPixelatedPosition(Module.Top);
            TorsoPresenter.SetPosition(position);
            transform.position = CameraStore.Pixelation.GetClosestPixelatedPosition(Module.Top);
        }
    }
}