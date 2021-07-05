using UnityEngine;
using UnityEngine.UI;
public enum State
{
    Waiting,
    Active,
    Completed
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;

    private Asteroid _asteroid;

    private int currentWave = 0;
    private void Start()
    {
        _asteroid = GameObject.Find("Asteroid").GetComponent<Asteroid>();
        if (_asteroid == null)
        {
            Debug.LogError("Asteroid script is missing");
        }

        _asteroid.OnAsteroidDestroyed += _asteroid_OnAsteroidDestroyed;
    }

    private void _asteroid_OnAsteroidDestroyed(object sender, System.EventArgs e)
    {
        StartWave();
        _asteroid.OnAsteroidDestroyed -= _asteroid_OnAsteroidDestroyed;
    }

    private void StartWave()
    {
     _waves[currentWave].SetActiveState();
    }
    
    private void Update()
    {
         if (_waves[currentWave].GetState() == State.Active)
         {
             _waves[currentWave].Update();
         }
         else if (_waves[currentWave].GetState() == State.Completed)
         {
             if (_waves.Length == currentWave + 1)
             {
                 Debug.Log("Bring on the boss!");
             }
             else
             {
                 currentWave++;
                 _waves[currentWave].SetActiveState();
             }
         }
    }

    [System.Serializable]
    private class Wave
    {
        [SerializeField] private EnemyBase[] _enemyArray;
        [SerializeField] private float _delayTimer;
        [SerializeField] private State _state = State.Waiting;
        [SerializeField] private Text _waveDescription;
    
       
        public void Update()
        {
            if (_delayTimer >= 0)
            {
                _delayTimer -= Time.deltaTime;
                if (_delayTimer <= 0  )
                {
                    SpawnEnemies();
                }
            }
    
            if (IsWaveOver())
            {
                _state = State.Completed;
            }
        }
        
        public void SpawnEnemies()
        {
            foreach (EnemyBase enemy in _enemyArray)
            {
                enemy.GetComponent<EnemyBase>().Spawn();
            }
        }
    
        public bool IsWaveOver()
        {
            if (_delayTimer < 0)
            {
                foreach (var enemy in _enemyArray)
                {
                    if (!enemy.IsDead())
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    
        public State GetState()
        {
            return _state;
        }
    
        public void SetActiveState()
        {
            _state = State.Active;
        }
    }
}


