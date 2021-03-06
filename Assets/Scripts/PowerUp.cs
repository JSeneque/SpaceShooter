using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    TripleShot,
    Speed,
    Shield,
    Ammo,
    Health,
    HeatSeeker,
    Burst,
    Negative
};

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUp;
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _lowerBound = -5.5f;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float attractorSpeedModifier = 2.0f;

    private bool _isAttracted = false;
    private Player _player;

    private void OnEnable()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("PowerUp - Player script is missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAttracted && _player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * attractorSpeedModifier* Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

            if (transform.position.y < _lowerBound)
            {
                Destroy(this.gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player script is missing!");
            }
            else
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
                        //Player player = other.GetComponent<Player>();
                        if (player != null)
                        {
                            player.ShieldOn();
                        }

                        break;
                    case PowerUpType.Ammo:
                        Debug.Log("Collected Ammo Power Up");

                        if (player != null)
                        {
                            player.UpdateAmmo(15);
                        }

                        break;
                    case PowerUpType.Health:
                        Debug.Log("Collected Health Power Up");

                        if (player != null)
                        {
                            player.Heal();
                        }

                        break;
                    case PowerUpType.HeatSeeker:
                        shootScript = other.GetComponent<PlayerShoot>();
                        if (shootScript != null)
                        {
                            shootScript.HeatSeekerActive();
                        }

                        break;
                    case PowerUpType.Burst:
                        shootScript = other.GetComponent<PlayerShoot>();
                        if (shootScript != null)
                        {
                            shootScript.BurstShootActive();
                        }
                        break;
                    case PowerUpType.Negative:
                        shootScript = other.GetComponent<PlayerShoot>();
                        if (shootScript != null)
                        {
                            shootScript.DisableWeapons();
                        }
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                }

                AudioSource.PlayClipAtPoint(_audioClip, transform.position);
                Destroy(this.gameObject);
            }
        }
    }

    public void BeingAttracted()
    {
        _isAttracted = true;
    }
}
