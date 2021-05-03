using UnityEngine;

public class Laser : MonoBehaviour
{
    // laser speed
    [SerializeField]
    private float _moveSpeed = 8f;

    [SerializeField]
    private float _upperBounds = 7f;

    // Update is called once per frame
    void Update()
    {
        // the laser should be up
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);

        // if the laser is off screen
        // destroy the game object
        if (transform.position.y > _upperBounds)
        {
            Destroy(gameObject);
        }
    }
}
