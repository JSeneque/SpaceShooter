using UnityEngine;

public enum EnemyMovementType
{
    Downwards,
    MoveRight,
    MoveLeft
}


public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 4.0f;
    [SerializeField] protected Transform _firePoint;
    [SerializeField] protected AudioClip _explosionAudioClip;
    [SerializeField] protected GameObject _explosionPrefab;
    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected EnemyMovementType _movementType;
    [SerializeField] protected float _fireRate = 3.0f;
    [SerializeField] protected int _lives = 1;
    [SerializeField] protected bool _horizontalWrapping;
    [SerializeField] protected bool _verticalWrapping;
    
    protected Player _player;
    protected Animator _animator;
    protected AudioSource _audioSource;
    protected float _canFire = -1.0f;
    protected bool _isDead;
    protected bool _targetlocked = false;
    protected bool _onScreen = false;
    
    protected float _direction = 0;
    
    public bool _offScreen;
    

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player script or player could note found");
        }
        
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL on Enemy");
        }
        else
        {
            _audioSource.clip = _explosionAudioClip;
        }
    }

    private void Damage()
    {
        _lives--;
        
        if (_lives <= 0)
        {
            OnEnemyDestroyAnimation();
        }
    }

    public void LockOnEnemy()
    {
        _targetlocked = true;
    }

    public bool IsEnemyLockedOn()
    {
        return _targetlocked;
    }

    public void Spawn()
    {
        this.gameObject.SetActive(true);
    }

    public bool IsDead()
    {
        return _isDead;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.Damage();
            Damage();
        }

        if(other.tag == "Laser")
        {
            if (other.GetComponent<Laser>().GetLaserType() == LaserType.Player)
            {
                Damage();
            }
        }
        
        if(other.tag == "Heat Seeker")
        {
            Damage();
        }
    }

    private void OnEnemyDestroyAnimation()
    {
        _player.AddScore(10);
        _isDead = true;
        _animator.SetTrigger("OnEnemyDeath");
        _moveSpeed = 0.5f;
        _audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.8f);
    }
    
    protected void CheckOnScreen()
    {
        if (transform.position.x < 10.0f && transform.position.x > -10.0f)
        {
            _onScreen = true;
        }
    }

    protected void CheckOffScreen()
    {
        if (transform.position.y < -5.0f)
        {
            _offScreen = true;
        }
    }
    
    protected void ScreenWrapping()
    {
        if (transform.position.y < -5f && _verticalWrapping)
        {
            float newXPosition = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(newXPosition, 10.0f, transform.position.z);
        }

        if ((transform.position.x > 10.0f || transform.position.x < -10.0f) && _onScreen && _horizontalWrapping)
        {
            float newYPosition = Random.Range(0.0f, 5.0f);

            if (transform.position.x > 10.0f)
            {
                transform.position = new Vector3(-10.0f, newYPosition, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(10.0f, newYPosition, transform.position.z);
            }
            
        }
    }
    
    public void ChangeDirection(float value)
    {
        _direction = value;
    }
}
