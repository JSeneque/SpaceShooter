using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private Transform _targetPosition;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(Transform position)
    {
        _targetPosition = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player missing Player script");
            }
            player.Damage();
            Destroy(this.gameObject);
        }
    }
}
