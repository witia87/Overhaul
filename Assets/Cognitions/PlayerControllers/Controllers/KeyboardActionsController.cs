using Assets.Gui.MainCamera;
using UnityEngine;

namespace Assets.Cognitions.PlayerControllers.Controllers
{
    public class KeyboardActionsController : IActionsController
    {
        public void Update()
        {
            IsDropWeaponPressed = Input.GetButtonDown("Drop Weapon");
            IsUsePressed = Input.GetButtonDown("Use");
        }

        public bool IsDropWeaponPressed { get; private set; }
        public bool IsUsePressed { get; private set; }
    }
}