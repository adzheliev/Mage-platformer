using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput
    {
        private PlayerControls _controls;
        private Player _player;
        private bool _forward;
        private Vector2 _inputVector;


        public PlayerInput(Player player)
        {
            _player = player;
            _controls = new PlayerControls();
            Bind();
        }

        private void Bind()
        {
            _controls.Player.Enable();

            _controls.Player.Move.started += OnMoveInput;
            _controls.Player.Move.canceled += OnMoveInput;
            _controls.Player.Jump.started += OnJumpInput;
            _controls.Player.Jump.canceled += OnJumpInput;
            _controls.Player.Fire.started += OnFireInput;
            _controls.Player.Fire.canceled += OnFireInput;

        }

        public void Expose()
        {
            _controls.Player.Disable();

            _controls.Player.Move.started -= OnMoveInput;
            _controls.Player.Move.canceled -= OnMoveInput;
            _controls.Player.Jump.started -= OnJumpInput;
            _controls.Player.Jump.canceled -= OnJumpInput;
            _controls.Player.Fire.started -= OnFireInput;
            _controls.Player.Fire.canceled -= OnFireInput;

        }

        private void OnFireInput(InputAction.CallbackContext context)
        {
            _player.SetFire(context.ReadValueAsButton());

        }

        private void OnJumpInput(InputAction.CallbackContext context)
        {
            _player.SetJump(context.ReadValueAsButton());

        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            _inputVector = context.ReadValue<Vector2>();
            if(_inputVector.x > 0)
            {
                _forward = true;
            }
            else if(_inputVector.x < 0)
            {
                _forward = false;
            }

            _player.SetInput(_inputVector, _forward);
        }
    



    }
}


