using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 _startPosition;

    [SerializeField]
    private float _moveSpeed = 3f;

    

    // Start is called before the first frame update
    void Start()
    {
        // set the players position at the start of the game
        transform.position = _startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();  
    }

    private void Move()
    {
        // get the player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // create a direction vector from the inputs
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0 );

        // move the cube
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }

    private void CheckBounds()
    {
        // if the player position on the y is greater than 5.3
        // the y position = 5.3
        // else if player position on th

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
