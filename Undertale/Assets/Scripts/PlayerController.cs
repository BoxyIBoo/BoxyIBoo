
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _lookAt;
    [SerializeField] private Transform _groundChekerPivot;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _checkGroundRadius = 0.4f;
    [SerializeField] private Animator anim;
    [SerializeField] private float sensitivity = 1f;

    private CharacterController _controller;
    private float _velocity;
    private Vector3 _moveDirection;
    private Vector2 turn;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        if (IsOnTheGround())
        {
            _velocity = -2;
        }

        Move(_moveDirection);
        DoGravity();
    }
    private void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        _lookAt.localRotation = Quaternion.Euler(-turn.y, 0, 0);
        _moveDirection = new Vector3(x: Input.GetAxis("Horizontal"), y: 0f, z: Input.GetAxis("Vertical"));

        if (_moveDirection != Vector3.zero) anim.SetBool("IsWalking", true);
        else anim.SetBool("IsWalking", false);
    }

    private bool IsOnTheGround()
    {
        bool result = Physics.CheckSphere(_groundChekerPivot.position, _checkGroundRadius, _groundMask);

        return result;
    }
    private void Move(Vector3 direction)
    {
        var dir = transform.TransformDirection(direction);
        _controller.Move(dir * _speed * Time.fixedDeltaTime);
    }
    private void DoGravity()
    {
        _velocity += _gravity * Time.fixedDeltaTime;

        _controller.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
    }
}
