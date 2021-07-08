using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RapidFighter : EnemyBase
{
    // [SerializeField] private float _moveSpeed = 4.0f;
    // [SerializeField] private Transform _firePoint;
    // [SerializeField] private float _fireRate = 1.0f;
    // [SerializeField] private GameObject _projectilePrefab;
     
     [SerializeField] private float _fireRange = 5.0f;
     [SerializeField] private float _delayBetweenShooting = 3.0f;
     [SerializeField] private int _numOfShots = 5;

     private Vector3 direction;
     private float _delayFire;
     private int countShots;
     private Transform playersLastPosition;
    private void Start()
    {
        direction = Vector3.right;
        
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
        
        countShots = _numOfShots;
    }

    private void Update()
    {
        Movement();
        Fire();
    }

    private void Movement()
    {
        if (transform.position.x < -8.0f )
        {
            direction = Vector3.right;
        } else if (transform.position.x > 8.0f)
        {
            direction = Vector3.left;
        }

        if (!_isDead)
        {
            transform.Translate(direction * _moveSpeed * Time.deltaTime);
        }
    }
    
    private void Fire()
    {
        if (Time.time > _delayFire && !_isDead)
        {
            if (Time.time > _canFire && !_isDead)
            {
                _canFire = Time.time + _fireRate;
                countShots--;

                if (countShots <= 0)
                {
                    _delayFire = Time.time + _delayBetweenShooting;
                    countShots = _numOfShots;
                }
                GameObject bullet = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
                bullet.GetComponent<FireBullet>().SetTarget(playersLastPosition);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            _isDead = true;
            Destroy(other.gameObject);
            // add 10 to the score
            if (_player != null)
            {
                _player.AddScore(10);
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 1.0f);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _fireRange);
    }
}
