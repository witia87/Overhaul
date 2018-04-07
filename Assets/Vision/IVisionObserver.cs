using Assets.Units;
using UnityEngine;

namespace Assets.Vision
{
    public interface IVisionObserver
    {
        bool GetClosestTarget(out ITarget target);
    }
}