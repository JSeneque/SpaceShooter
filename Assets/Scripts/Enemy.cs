using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 4.0f;

    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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

            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            Destroy(other);
            // add 10 to the score
            if (_player != null)
            {
                _player.AddScore(10);
            }
            Destroy(this.gameObject);
        }

    }
}
