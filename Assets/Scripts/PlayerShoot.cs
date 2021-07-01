using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShot;
    [SerializeField] private GameObject _heatSeekerPrefab;
    [SerializeField] private GameObject _burstPrefab;
    [SerializeField] private float _firepointOffset = 1.07f;
    [SerializeField] 
    [Range(0,5)]
    private float _fireRate = 0.5f;

    [SerializeField] private AudioClip _laserSFX;
    
    private float _canFire = -1f;
    private bool _isTripleShotActive = false;
    private bool _isHeatSeekerActive = false;
    private bool _isBurstShotActive = false;
    private bool _weaponsDisabled = false;
    private AudioSource _audioSource;
    private Player _player;
    

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

        _player = GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player script is missing!");
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _player.GetAmmoCount() > 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (!_weaponsDisabled)
        {
            _canFire = Time.time + _fireRate;

            Vector3 firePoint;
            
            if (_isTripleShotActive)
            {
                firePoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Instantiate(_tripleShot, firePoint, Quaternion.identity);
                _player.UpdateAmmo(-1);
            }
            else if (_isHeatSeekerActive)
            {
                firePoint = new Vector3(transform.position.x, transform.position.y + _firepointOffset,
                    transform.position.z);
                GameObject _laser = Instantiate(_heatSeekerPrefab, firePoint, Quaternion.identity);
                //_player.UpdateAmmo(-1);
            }
            else if (_isBurstShotActive)
            {
                firePoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Instantiate(_burstPrefab, firePoint, Quaternion.identity);
                //_player.UpdateAmmo(-1);
            }
            else
            {
                firePoint = new Vector3(transform.position.x, transform.position.y + _firepointOffset,
                    transform.position.z);
                GameObject _laser = Instantiate(_laserPrefab, firePoint, Quaternion.identity);
                _player.UpdateAmmo(-1);
            }

            _audioSource.Play();
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerUpCoolDownRoutine());
    }
    
    public void HeatSeekerActive()
    {
        _isHeatSeekerActive = true;
        StartCoroutine(HeatSeekerPowerUpCoolDownRoutine());
    }

    public void BurstShootActive()
    {
        _isBurstShotActive = true;
        StartCoroutine(BurstShotPowerUpCoolDownRoutine());
    }

    IEnumerator TripleShotPowerUpCoolDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    
    IEnumerator HeatSeekerPowerUpCoolDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isHeatSeekerActive = false;
    }
    
    IEnumerator BurstShotPowerUpCoolDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isBurstShotActive = false;
    }
    
    public void DisableWeapons()
    {
        _weaponsDisabled = true;
        StartCoroutine(DisableWeaponsPowerUpCoolDownRoutine());
    }
    
    IEnumerator DisableWeaponsPowerUpCoolDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _weaponsDisabled = false;
    }
}
