using Assets.Units;
using Assets.Units.Heads.Vision;
using UnityEngine;

namespace Assets.Vision
{
    public interface IVisionObserver
    {
        bool GetClosestTarget(out ITarget target);
        void SwitchSidesOfConflict();
    }
}