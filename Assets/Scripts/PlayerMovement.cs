using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 _startPosition;

    [SerializeField]
    private float _moveSpeed = 3f;

    void Start()
    {
        transform.position = _startPosition;
    }

    void Update()
    {
        Move();
        CheckBounds();  
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0 );

        transform.Translate(direction * _moveSpeed * Time.deltaTime);
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
}
