using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    TripleShot,
    Speed,
    Shield
};

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUp;
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _lowerBound = -5.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

        if (transform.position.y < _lowerBound)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch (powerUp)
            {
                case PowerUpType.TripleShot:
                    PlayerShoot shootScript = other.GetComponent<PlayerShoot>();
                    if (shootScript != null)
                    {
                        shootScript.TripleShotActive();
                    }
                    break;
                case PowerUpType.Speed:
                    Debug.Log("Collect Speed Power Up");
                    PlayerMovement movementScript = other.GetComponent<PlayerMovement>();
                    if (movementScript != null)
                    {
                        movementScript.SpeedBoost();
                    }
                    break;
                case PowerUpType.Shield:
                    Debug.Log("Collected Shield Power Up");
                    break;
                default:
                    Debug.Log("Default value");
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
