using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4.0f;
    [SerializeField] private AudioClip _explosionAudioClip;
    
    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;

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
        _animator.SetTrigger("OnEnemyDeath");
        _moveSpeed = 0.5f;
        _audioSource.Play();
        Destroy(this.gameObject, 2.8f);
    }
}
