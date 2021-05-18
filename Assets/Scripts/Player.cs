using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject _shieldVisualiser;
    [SerializeField] private int _score;
    
    private SpawnManager _spawnManager;
    private bool _isShieldActive;
    private UIManager _uIManager;
    

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manger is NULL");
        }

        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uIManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

    public void Damage()
    {
        if (!_isShieldActive)
        {
            _lives--;
            
            _uIManager.UpdateLives(_lives);
            
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
    
    // create method to add 10 to the score
    // communicate to the UIManager to added the score display
    public void AddScore(int amount)
    {
        _score += amount;
        _uIManager.UpdateScoreText(_score);
    }
}
