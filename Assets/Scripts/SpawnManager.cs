using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private float _enemySpawnRate = 5.0f;
    [SerializeField] private float _powerSpawnRateMin = 7.0f;
    [SerializeField] private float _powerSpawnRateMax = 10.0f;

    //[SerializeField] private PowerUpType powerUp;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        
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
        yield return new WaitForSeconds(3.0f);
        
        while (_stopSpawning == false)
        {
            Vector3 newPosition = new Vector3(Random.Range(-8.0f, 8.0f), 10.0f, 0f);
            int randomPowerUp = Random.Range(0, _powerUps.Length);
            
            Instantiate(_powerUps[randomPowerUp], newPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(_powerSpawnRateMin, _powerSpawnRateMax));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    
}
