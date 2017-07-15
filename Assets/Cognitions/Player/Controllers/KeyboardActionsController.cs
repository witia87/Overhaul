using UnityEngine;

namespace Assets.Cognitions.Player.Controllers
{
    public class KeyboardActionsController : IActionsController
    {
        public bool IsDropWeaponPressed { get; private set; }
        public bool IsUsePressed { get; private set; }

        public void Update()
        {
            IsDropWeaponPressed = Input.GetButtonDown("Drop Weapon");
            IsUsePressed = Input.GetButtonDown("Use");
        }
    }
}