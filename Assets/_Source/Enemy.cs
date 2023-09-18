using Supporting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed, _raycastDistance;
    [SerializeField] private Transform _leftPoint, _rightPoint;
    [SerializeField] private int _damageToPlayer;
    [SerializeField] private SpriteRenderer _sprite;
    [Space(10)]
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _playerBulletLayer;
    [SerializeField] private LayerMask _groundLayer;

    private bool _direction;
    private RaycastHit2D _leftHit;
    private RaycastHit2D _rightHit;

    private void Awake()
    {
        if(_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        CheckMove();
    }

    private void CheckMove()
    {
        _leftHit = Physics2D.Raycast(_leftPoint.position, Vector2.down, _raycastDistance, _groundLayer);
        _rightHit = Physics2D.Raycast(_rightPoint.position, Vector2.down, _raycastDistance, _groundLayer);

        if(_leftHit.collider == null)
        {
            _direction = true;
            _sprite.flipX = true;
        }
        else if(_rightHit.collider == null)
        {
            _direction = false;
            _sprite.flipX = false;
        }

        _rigidbody.velocity = _direction ? Vector2.right * _speed : Vector2.left * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Utils.IsInLayer(collision.gameObject.layer, _playerBulletLayer))
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }

    public int GetDamage()
    {
        return -_damageToPlayer;
    }
}
