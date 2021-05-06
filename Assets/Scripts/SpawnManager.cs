using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        // while loop (infinite loop)
            // Instantiate enemy prefab
            // yield wait for 5 seconds

        while (true)
        {
            Vector3 newPosition = new Vector3(Random.Range(-8.0f, 8.0f), 10.0f, 0);

            Instantiate(_enemyPrefab, newPosition, Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }

    }
}
