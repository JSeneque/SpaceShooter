using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4.0f;
    [SerializeField] private AudioClip _explosionAudioClip;
    [SerializeField] private GameObject _laserPrefab;
    
    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;
    private bool _isDead;

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
        CalculateMovement();

        if (Time.time > _canFire && !_isDead)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject laserGO = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = laserGO.GetComponentsInChildren<Laser>();

            foreach (var laser in lasers)
            {
                laser.SetIsPlayer(false);
            }

        }
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            float newXPosition = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(newXPosition, 10.0f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            OnEnemyDestroyAnimation();
            
        }

        if(other.tag == "Laser")
        {
            Destroy(other);
            // add 10 to the score
            if (_player != null)
            {
                _player.AddScore(10);
            }
            OnEnemyDestroyAnimation();
        }

    }
    
    private void OnEnemyDestroyAnimation()
    {
        _isDead = true;
        _animator.SetTrigger("OnEnemyDeath");
        _moveSpeed = 0.5f;
        _audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.8f);
    }
}
