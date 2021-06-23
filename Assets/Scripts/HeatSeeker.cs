using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSeeker : MonoBehaviour
{
    [SerializeField] private float detectionZone;
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private float _upperBounds = 7f;
    [SerializeField] private float _lowerBounds = -5f;
    [SerializeField] private float _leftBounds = -10f;
    [SerializeField] private float _rightBounds = 10f;
    [SerializeField] private float angularSpeed = 360f;
    
    private WaitForSeconds heatSeekUpdate = new WaitForSeconds(0.5f);
    private Collider2D[] enemies = new Collider2D[25];
    private float nearestEnemyDistance;
    private Enemy nearestEnemy;
    private Vector2 enemyDirection = Vector2.zero;
    private void OnEnable()
    {
        StartCoroutine(HeatSeek());
    }
    
    private void Update()
    {

        if (nearestEnemy != null)
        {
            enemyDirection = (nearestEnemy.transform.position - transform.position).normalized;
            transform.Rotate(0f, 0f, Vector3.Cross(enemyDirection, transform.up).z * -1 * angularSpeed * Time.deltaTime);
            
        }
        
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);

        if (transform.position.y > _upperBounds || transform.position.y < _lowerBounds || transform.position.x < _leftBounds || transform.position.x > _rightBounds )
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator HeatSeek()
    {
        while (true)
        {
            enemies = Physics2D.OverlapCircleAll(transform.position,detectionZone, (1 << 9));
            //Debug.Log("Number of Entities: " + numOfEntities);
            nearestEnemyDistance = detectionZone;
            
            foreach(var entity in enemies)
            {
                 if (entity.gameObject.TryGetComponent(out Enemy enemy) )
                 {
                     if (!enemy.IsEnemyLockedOn())
                     {
                         float distance = Vector2.Distance(transform.position, enemy.transform.position);

                         if (nearestEnemyDistance > distance)
                         {
                             //Debug.Log("Enemy found!");
                             nearestEnemy = enemy;
                             nearestEnemyDistance = distance;
                             enemy.LockOnEnemy();
                         }
                     }
                 }
            }
            
            yield return heatSeekUpdate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionZone);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
