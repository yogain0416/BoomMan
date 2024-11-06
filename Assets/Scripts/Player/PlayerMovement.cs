using App.UI;
using UnityEngine;

namespace App.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private FixedJoystick _joyStick;
        [SerializeField] private float _speed;
        
        private float _moveHorizontal, _moveVertical;
        private bool _isBlockingMovement = false;
        public bool BlockMovement { get { return _isBlockingMovement; } set {  _isBlockingMovement = value; } }

        public void KeyBoardMove()
        {
            if (_isBlockingMovement == true) return;

            _moveHorizontal = Input.GetAxis("Horizontal");        // 가로축
            _moveVertical = Input.GetAxis("Vertical");          // 세로축

            Vector3 move = transform.right * _moveHorizontal + transform.forward * _moveVertical;
            _rigidbody.velocity = new Vector3(move.x * _speed, _rigidbody.velocity.y, move.z * _speed);
        }

        public void JoyStickMove()
        {
            if (_isBlockingMovement == true) return;

            _rigidbody.velocity = new Vector3(_joyStick.Horizontal * _speed, _rigidbody.velocity.y, _joyStick.Vertical * _speed);
        }

        // TODO Rotate
    }
}
