using System;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _upperBounds = 7f;
    [SerializeField] private float _lowerBounds = -5f;
    [SerializeField] private bool _isPlayer = true;
    void Update()
    {
        if (_isPlayer)
        {
            transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        }

        if (transform.position.y > _upperBounds || transform.position.y < _lowerBounds)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    public void SetIsPlayer(bool value)
    {
        _isPlayer = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if collider with player, destroy player and laser
        // if an enemy laser
        //  if collider with enemy
        //      nothing happens
        if (other.tag == "Player" && !_isPlayer)
        {
            Player player = other.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player missing Player script");
            }
            player.Damage();
            
            Destroy(gameObject);
        } 
        
    }
    
}
