using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private bool _isSpeedBoostActive;
    

    private float _speedMultiplier = 2.0f;
    private float _thrusterMultiplier = 1.5f;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _isThrusterActive;
    
    void Start()
    {
        transform.position = _startPosition;
    }

    void Update()
    {
        GetInputs();
        Move();
        CheckBounds();  
    }

    private void GetInputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isThrusterActive = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isThrusterActive = false;
        }
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_horizontalInput, _verticalInput, 0 );

        if (_isSpeedBoostActive)
        {
            transform.Translate(direction * _moveSpeed * _speedMultiplier * Time.deltaTime);
        }
        else if (_isThrusterActive)
        {
            transform.Translate(direction * _moveSpeed * _thrusterMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _moveSpeed  * Time.deltaTime);
        }
        
    }

    private void CheckBounds()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.3f, 5.3f));

        if (transform.position.x < -9.37)
        {
            transform.position = new Vector3(9.37f, transform.position.y, 0);
        }
        else if (transform.position.x > 9.37)
        {
            transform.position = new Vector3(-9.37f, transform.position.y, 0);
        }
    }

    public void SpeedBoost()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostRoutine());
    }

    IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        _isSpeedBoostActive = false;
    }
    
}
