using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _lives = 3;

    public void Damage()
    {
        _lives--;

        if(_lives <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
