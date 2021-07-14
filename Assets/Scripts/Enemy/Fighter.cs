using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Fighter : EnemyBase
{
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        if (_animator == null)
        {
            Debug.LogError("Enemy GFX Animator is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL on Enemy");
        }
        else
        {
            _audioSource.clip = _explosionAudioClip;
        }
    }

    void Update()
    {
        Movement();
        ScreenWrapping();
        CheckOnScreen();
        Fire();
    }

    private void Fire()
    {
        if (Time.time > _canFire && !_isDead)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject laserGO = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
            Laser[] lasers = laserGO.GetComponentsInChildren<Laser>();

             foreach (var laser in lasers)
             {
                 laser.SetLaserType(LaserType.FireDown);
             }

        }
    }

    private void Movement()
    {
        if (_movementType == EnemyMovementType.Downwards)
        {
            transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        }
        else if (_movementType == EnemyMovementType.MoveRight)
        {
            transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
        }
        else if (_movementType == EnemyMovementType.MoveLeft)
        {
            transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
        }
    }

    private void ScreenWrapping()
    {
        if (transform.position.y < -4.5f)
        {
            float newXPosition = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(newXPosition, 10.0f, transform.position.z);
        }

        if ((transform.position.x > 10.0f || transform.position.x < -10.0f) && _onScreen)
        {
            float newYPosition = Random.Range(0.0f, 5.0f);

            if (transform.position.x > 10.0f)
            {
                transform.position = new Vector3(-10.0f, newYPosition, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(10.0f, newYPosition, transform.position.z);
            }
            
        }
    }

}
