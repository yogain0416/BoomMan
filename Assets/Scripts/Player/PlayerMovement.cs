using App.UI;
using UnityEngine;

namespace App.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private FloatingJoystick _joyStick;
        [SerializeField] private float _speed;
        
        private bool _isBlockingMovement = false;
        private bool _isKeyBord = true;
        public bool KeyBoardMoveMent { get { return _isKeyBord; } set { _isKeyBord = value; } }
        public bool BlockMovement { get { return _isBlockingMovement; } set {  _isBlockingMovement = value; } }

        public void MovePlayer()
        {
            if (_isBlockingMovement) return; // 이동이 차단되었으면 실행하지 않음

            Vector3 movement;

            // 1. 조이스틱 입력이 있는 경우 조이스틱으로 이동
            if (_joyStick.Horizontal != 0 || _joyStick.Vertical != 0)
            {
                movement = new Vector3(_joyStick.Horizontal, 0, _joyStick.Vertical) * _speed;
                _isKeyBord = false;
            }
            // 2. 조이스틱 입력이 없을 때 키보드 입력을 사용
            else
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                movement = new Vector3(moveHorizontal, 0, moveVertical) * _speed;
                _isKeyBord = true;
            }

            // Rigidbody를 이용해 이동 속도 적용
            _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);

            // 움직임이 있는 경우에만 회전
            if (movement != Vector3.zero)
            {
                RotatePlayer(movement);
            }

            // 애니메이터의 Speed 파라미터 업데이트
            float currentSpeed = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z).magnitude;
            _animator.SetFloat("Speed", currentSpeed);
        }

        private void RotatePlayer(Vector3 direction)
        {
            // 현재 위치에서 이동 방향을 바라보는 회전 설정
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
        }
    }
}
