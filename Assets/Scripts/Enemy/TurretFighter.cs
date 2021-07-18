using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurretFighter : EnemyBase
{
    [SerializeField] private float _offset = 0.5f;
    [SerializeField] private Transform _turret;
    private float elapsedTime = 0f;
    
    public bool isBehindPlayer;
    
    private void Update()
    {
        Movement();
        CheckIfBehindPlayer();
        CheckOffScreen();
        RemoveEnemy();
        Fire();
    }

    private void Movement()
    {
        //float x = 0.1f * Mathf.Sin(_moveSpeed * elapsedTime);

        //transform.Translate(new Vector3(x, -_moveSpeed * Time.deltaTime));
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        elapsedTime += Time.deltaTime;
    }
    
    private void Damage()
    {
        _lives--;
        
        if (_lives <= 0)
        {
            OnEnemyDestroy();
        }
    }

    private void OnEnemyDestroy()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        _isDead = true;

        if (_player != null)
        {
            _player.AddScore(10);
        }

        _player.Damage();
        _moveSpeed = 0;
        _audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject, 2.5f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Damage();
        }

        if (other.tag == "Laser" || other.tag == "Heat Seeker")
        {
            Damage();
            Destroy(other.gameObject);
        }
    }

    private void RemoveEnemy()
    {
        if (_offScreen)
        {
            Destroy(gameObject);
        }
    }

    private void CheckIfBehindPlayer()
    {
        if (!_player.GetIsDead())
        {
            isBehindPlayer = (_player.transform.position.y > gameObject.transform.position.y - _offset) ? true : false;
        }
    }
    
    private void Fire()
    {
        if (isBehindPlayer && _player != null)
        {
            if (Time.time > _canFire && !_isDead)
            {
                _fireRate = Random.Range(0.5f, 1.0f);
                _canFire = Time.time + _fireRate;
                _firePoint = gameObject.transform.Find("Turret").Find("FirePoint");
                GameObject laserGO = Instantiate(_projectilePrefab, _firePoint.position, _turret.rotation);
                Laser[] lasers = laserGO.GetComponentsInChildren<Laser>();

                foreach (var laser in lasers)
                {
                    laser.SetLaserType(LaserType.TowardsTarget);
                }
            }
        }
        
    }
}
