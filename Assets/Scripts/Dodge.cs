using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    [SerializeField] private float _dodgeSpeed = 2f;
    
    private EnemyBase _enemyBase;
    
    private void OnEnable()
    {
        _enemyBase = transform.parent.GetComponent<EnemyBase>();
        if (_enemyBase == null)
        {
            Debug.LogError("EmemyBase script is missing");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (other.GetComponent<Laser>().GetLaserType() == LaserType.Player)
            {
                if (other.transform.position.x < transform.position.x)
                {
                    _enemyBase.ChangeDirection(_dodgeSpeed);
                }
                else
                {
                    _enemyBase.ChangeDirection(-_dodgeSpeed);
                }
            }
        }
    }
}
