using Assets.Gui.Player;
using UnityEngine;

namespace Assets.Gui.PlayerInput
{
    public class TargetingPlaneComponent : MonoBehaviour
    {
        private IPlayerStore _playerStore;

        private void Awake()
        {
            _playerStore = FindObjectOfType<PlayerStore>();
        }

        private void Update()
        {
            transform.position = _playerStore.PlayerUnit.Gun.Position;
        }
    }
}