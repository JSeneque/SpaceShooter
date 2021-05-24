using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShot;
    [SerializeField] private float _firepointOffset = 1.07f;
    [SerializeField] 
    [Range(0,5)]
    private float _fireRate = 0.5f;

    [SerializeField] private AudioClip _laserSFX;
    
    private float _canFire = -1f;
    private bool _isTripleShotActive = false;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("Audio source on the Player is null!");
        }
        else
        {
            _audioSource.clip = _laserSFX;
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Fire();
        }
    }

    private void Fire()
    {
        _canFire = Time.time + _fireRate;

        Vector3 firePoint;

        if (_isTripleShotActive)
        {
            firePoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(_tripleShot, firePoint, Quaternion.identity);
        }
        else
        {
            firePoint = new Vector3(transform.position.x, transform.position.y + _firepointOffset, transform.position.z);
            Instantiate(_laserPrefab, firePoint, Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotCooldownRoutine());
    }

    // IEnumerator TripleShotPowerDownRoutine
    // wait 5 second
    // set te triple shot to false

    IEnumerator TripleShotCooldownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        _isTripleShotActive = false;
    }
}
