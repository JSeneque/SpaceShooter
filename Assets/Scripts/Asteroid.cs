using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 3.0f;
    [SerializeField] private GameObject _explosionPrefab;

    public event EventHandler OnAsteroidDestroyed;
    
    //private SpawnManager _spawnManager;
    private CameraShake _cameraShake;
    
    // Start is called before the first frame update
    void Start()
    {
        // _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        // if (_spawnManager == null)
        // {
        //     Debug.LogError("Spawn Manager is missing");
        // }

        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        if (_cameraShake == null)
        {
            Debug.LogError("The main camera is missing the camera shake script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            //_spawnManager.StartSpawning();
            Debug.Log("Asteroid Destroyed!");
            OnAsteroidDestroyed?.Invoke(this, EventArgs.Empty);
            StartCoroutine(_cameraShake.Shake(0.50f,0.15f));
            Destroy(this.gameObject, 1.0f);
        }

    }
}
