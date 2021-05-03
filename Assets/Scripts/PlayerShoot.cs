using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _firepointOffset = 0.8f;

    [SerializeField]
    [Range(0,5)]
    private float _fireRate = 0.5f;

    private float _canFire = -1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Fire();
        }
    }

    private void Fire()
    {
        _canFire = Time.time + _fireRate;

        Vector3 firePoint = new Vector3(transform.position.x, transform.position.y + _firepointOffset, transform.position.z);

        Instantiate(_laserPrefab, firePoint, Quaternion.identity);
    }
}
