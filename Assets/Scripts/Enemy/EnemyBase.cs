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
    
    protected Player _player;
    protected Animator _animator;
    protected AudioSource _audioSource;
    protected float _canFire = -1.0f;
    protected bool _isDead;
    protected bool _targetlocked = false;
    protected bool _onScreen = false;

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
            OnEnemyDestroyAnimation();
        }

        if(other.tag == "Laser")
        {
            if (other.GetComponent<Laser>().GetIsPlayer())
            {
                OnEnemyDestroyAnimation();
            }
        }
        
        if(other.tag == "Heat Seeker")
        {
            OnEnemyDestroyAnimation();
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
}
