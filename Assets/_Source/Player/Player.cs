using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [Header("Numeric values")]
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _fireDelay;
        [SerializeField] private int _bulletAmount;
        [SerializeField] private int _startHP;

        [Header("Requirement components")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _groundPoint;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private HealthView _healthView;
        
        [Header("Physical layers")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _bonusLayer;
        [SerializeField] private LayerMask _finishLayer;
        [SerializeField] private LayerMask _deathLayer;

        [Space(15)]
        [SerializeField] private GameObject _bulletPrefab;


        private PlayerMovement _movement;
        private PlayerInput _input;
        private PlayerAnimator _playerAnimator;
        private PlayerFiring _playerFiring;
        private Health _health;

        private Vector2 _inputVector;
        private bool _directionForward, _grounded, _jumping, _firing;
        private Vector3 _scaleForward, _scaleBackward;

        private void Awake()
        {
            if(_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody2D>();
            }

            if(_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            InitScaleAndDirection();

            _input = new PlayerInput(this);
            _movement = new PlayerMovement(_rigidbody);
            _playerAnimator = new PlayerAnimator(_animator);
            _playerFiring = new PlayerFiring(_fireDelay, _bulletAmount, _bulletPrefab);
            _health = new Health(_healthView, _startHP);
        }

        private void Update()
        {
            _playerAnimator.SetMove(_inputVector.x);
            _playerAnimator.SetGrounded(_grounded);
            _playerFiring.TryFiring(Time.deltaTime, _firing, _firePoint.position, _directionForward);        
        }

        private void FixedUpdate()
        {
            _grounded = Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
            _movement.Move(_inputVector.x * _speed);
            _movement.Jump(_jumping, _grounded, _jumpForce);
        }

        private void OnDestroy()
        {
            _input.Expose();

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Utils.IsInLayer(collision.gameObject.layer, _enemyLayer))
            {
                _health.ChangeHealth(collision.gameObject.GetComponent<Enemy>().GetDamage());
            }

        }

        private void InitScaleAndDirection()
        {
            _directionForward = true;
            _scaleForward = transform.localScale;
            _scaleBackward = _scaleForward;
            _scaleBackward.x *= -1;
        }

        public void SetJump (bool jumping)
        {
            _jumping = jumping;
        }


        public void SetInput (Vector2 inputVector, bool forward)
        {
            _inputVector = inputVector;
            Flip(forward);
        }

        public void SetFire(bool firing)
        {
            _firing = firing;
        }

        private void Flip(bool direction)
        {
            transform.localScale = direction ? _scaleForward : _scaleBackward;
            _directionForward = direction;
        }
    }
}


