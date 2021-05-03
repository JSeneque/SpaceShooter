using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _firepointOffset = 0.8f;

    // Update is called once per frame
    void Update()
    {
        // if I hit the spacekey
        // spawn gameobject

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 firePoint = new Vector3(transform.position.x, transform.position.y + _firepointOffset, transform.position.z);

            Instantiate(_laserPrefab, firePoint, Quaternion.identity);
        }
    }
}
