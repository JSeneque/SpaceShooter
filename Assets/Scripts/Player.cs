using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject _shieldVisualiser;
    
    private SpawnManager _spawnManager;
    private bool _isShieldActive;
    

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manger is NULL");
        }
    }

    public void Damage()
    {
        if (!_isShieldActive)
        {
            _lives--;
            if(_lives <= 0)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }  
        }
        else
        {
            _isShieldActive = false;
            _shieldVisualiser.SetActive(false);
        }
    }

    public void ShieldOn()
    {
        _isShieldActive = true;
        _shieldVisualiser.SetActive(true);
    }
}
