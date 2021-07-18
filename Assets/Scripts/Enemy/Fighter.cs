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
        if (Time.time > _canFire && !_isDead && _player != null)
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
            transform.Translate(Vector3.right * _direction * _moveSpeed * Time.deltaTime);
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

    

}
