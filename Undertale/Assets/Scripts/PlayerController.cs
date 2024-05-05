using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _groundChekerPivot;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _checkGroundRadius = 0.4f;
    private CharacterController _controller;
    private float _velocity;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _controller = GetComponent<CharacterController> ();
    }
    private void FixedUpdate()
    {
        if (IsOnTheGround()){
            _velocity = -2;
        }

        Move(_moveDirection);
        DoGravity();
    }
    private void Update()
    {
        _moveDirection = new Vector3(x: -Input.GetAxis("Vertical"), y: 0f, z: Input.GetAxis("Horizontal") );
    }

    private bool IsOnTheGround()
    {
        bool result = Physics.CheckSphere(_groundChekerPivot.position , _checkGroundRadius , _groundMask);

        return result;
    }
    private void Move(Vector3 direction)
    {
        _controller.Move (direction * _speed * Time.fixedDeltaTime);
    }
    private void DoGravity()
    {
        _velocity += _gravity * Time.fixedDeltaTime;

        _controller.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
    }
}





































































































































































































