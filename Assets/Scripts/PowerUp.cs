using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3.0f;

    [SerializeField]
    private float _lowerBound = -5.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

        if (transform.position.y < _lowerBound)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerShoot shootScript = other.GetComponent<PlayerShoot>();
            if (shootScript != null)
            {
                shootScript.TripleShotActive();
            }

            Destroy(this.gameObject);
        }
    }
}
