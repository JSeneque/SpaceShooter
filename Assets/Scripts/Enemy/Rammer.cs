using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Rammer : EnemyBase
{
    [SerializeField] private float _rammingRange = 5.0f;
    [SerializeField] private float _speedModifier = 1.5f;

    private bool _isRamming = false;
    private Vector3 _playerPosition = Vector3.zero;
    private bool _hasArrived = false;

    // Update is called once per frame
    void Update()
    {
        InRammingRange();
        Movement();
    }

    private void InRammingRange()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position );
        
        if (!_isRamming)
        {
            if (distance < _rammingRange)
            {
                _isRamming = true;
                _playerPosition = _player.transform.position;
            }
        }
    }

    private void Movement()
    {
        if (_isRamming && !_hasArrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerPosition, _moveSpeed *_speedModifier * Time.deltaTime);
        }
        else if (!_isRamming || (_isRamming && _hasArrived))
        {
            transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        }
        
        float distance = Vector3.Distance(transform.position, _playerPosition );

        if (distance < 0.1f)
        {
            _hasArrived = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _isDead = true;
            // add 10 to the score
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rammingRange);
    }
}
