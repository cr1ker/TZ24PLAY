using UnityEngine;

public class PlayerMovingController : MonoBehaviour
{
    [SerializeField] private float _movingSpeed;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _player;
    [SerializeField] private Animator _jumpAnimation;
    [SerializeField] private float _jumpForce;
    private string _jumpTrigger = "Jump";
    
    public void DoPlayerJump()
    {
        _jumpAnimation.SetTrigger(_jumpTrigger);
        _player.position += Vector3.up * _jumpForce;
        _jumpAnimation.SetTrigger(_jumpTrigger);
    }

    private void Update()
    {
        if (_joystick.Horizontal != 0)
        {
            _player.position = new Vector3(_joystick.Horizontal * _movingSpeed, _player.transform.position.y, 0);
        }
    }
}
