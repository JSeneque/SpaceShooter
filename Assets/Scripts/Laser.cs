using System;
using UnityEngine;

public enum LaserType
{
    Player,
    FireDown,
    TowardsTarget
}

public class Laser : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _upperBounds = 7f;
    [SerializeField] private float _leftBounds = -10f;
    [SerializeField] private float _rightBounds = 10.5f;
    [SerializeField] private float _lowerBounds = -5f;
    [SerializeField] private LaserType _laserType;
    [SerializeField] private float angularSpeed = 360f;
    
    private Vector3 _targetPosition = Vector3.zero;
    private Vector3 _targetDirection = Vector3.zero;
    private bool _targetLocked = false;
    private Player _player;
    
    private void OnEnable() => _player = GameObject.Find("Player").GetComponent<Player>();
    
    public LaserType GetLaserType() => _laserType;
    public void SetLaserType(LaserType value) => _laserType = value;

    void Update()
    {
        Movement();
        CheckBounds();
    }

    private void CheckBounds()
    {
        if (transform.position.y > _upperBounds || transform.position.y < _lowerBounds ||
            transform.position.x > _rightBounds || transform.position.x < _leftBounds)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    private void Movement()
    {
        if (_player != null)
        {
            switch (_laserType)
            {
                case LaserType.Player:
                    transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
                    break;
                case LaserType.FireDown:
                    transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
                    break;
                case LaserType.TowardsTarget:
                    if (!_targetLocked && _player != null)
                    {
                        _targetLocked = true;
                        _targetPosition = _player.transform.position;
                    }

                    if (_targetPosition != null)
                    {
                        _targetDirection = (_targetPosition - transform.position).normalized;
                        transform.Rotate(0f, 0f,
                            Vector3.Cross(_targetDirection, transform.up).z * -1 * angularSpeed * Time.deltaTime);
                    }

                    transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _laserType != LaserType.Player)
        {
            if (_player != null)
            {
                _player.Damage();
            }
            Destroy(gameObject);
        }
        
        
    }
}
