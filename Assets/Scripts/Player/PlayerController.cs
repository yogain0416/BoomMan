using UnityEngine;

namespace App.Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private PlayerMovement _playerMovement;
        
        private void FixedUpdate()
        {
            _playerMovement.MovePlayer();
        }
    }
}
