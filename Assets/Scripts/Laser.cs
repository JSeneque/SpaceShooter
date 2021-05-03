using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 8f;

    [SerializeField]
    private float _upperBounds = 7f;

    void Update()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);

        if (transform.position.y > _upperBounds)
        {
            Destroy(gameObject);
        }
    }
}
