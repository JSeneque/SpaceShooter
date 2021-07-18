using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    // IF C key is pressed THEN
    //      IF powers within range THEN
    //          each power move towards player
    //      END IF
    // END IF
    [SerializeField] private float detectionZone;

    private Player _player;
    private Collider2D[] _powerUps;

    private void OnEnable()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player script is missing!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // check all the power ups in range
            _powerUps = Physics2D.OverlapCircleAll(transform.position,detectionZone, (1 << 11));

            foreach (var powerUp in _powerUps)
            {
                powerUp.GetComponent<PowerUp>().BeingAttracted();
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionZone);
    }
}
