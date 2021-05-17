using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _powerUpPrefab;
    [SerializeField] private float _enemySpawnRate = 5.0f;
    [SerializeField] private float _powerSpawnRateMin = 7.0f;
    [SerializeField] private float _powerSpawnRateMax = 10.0f;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 newPosition = new Vector3(Random.Range(-8.0f, 8.0f), 10.0f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, newPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
        
            yield return new WaitForSeconds(_enemySpawnRate);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 newPosition = new Vector3(Random.Range(-8.0f, 8.0f), 10.0f, 0f);
            Instantiate(_powerUpPrefab, newPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(_powerSpawnRateMin, _powerSpawnRateMax));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    
}
