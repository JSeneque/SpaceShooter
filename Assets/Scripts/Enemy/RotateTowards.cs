using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    private float angularSpeed = 360f;
    private Player _player;

    private void OnEnable() => _player = GameObject.Find("Player").GetComponent<Player>();

    private void Update()
    {
        if (_player)
        {
            transform.Rotate(0f, 0f, Vector3.Cross(_player.transform.position, transform.up).z * -1 * angularSpeed * Time.deltaTime);
        }
    }
}
