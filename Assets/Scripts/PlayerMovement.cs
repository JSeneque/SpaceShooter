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
        // get the player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // create a direction vector from the inputs
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0 );

        // move the cube
        transform.Translate(direction * _moveSpeed * Time.deltaTime); 
    }
}
