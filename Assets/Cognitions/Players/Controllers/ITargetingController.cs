
using Assets.Units.Modules;
using UnityEngine;

namespace Assets.Cognitions.Players.Controllers
{
    public interface ITargetingController
    {
        Vector3 TargetedPosition { get; }
        bool IsTargetPresent { get; }
        Module TargetedModule { get; }

        bool IsFirePressed { get; }
    }
}