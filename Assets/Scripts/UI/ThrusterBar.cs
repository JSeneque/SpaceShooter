using UnityEngine;
using UnityEngine.UI;

public class ThrusterBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _regenerateRate = 4f;

    private bool _isThrusting = false;
    
    private void Start()
    {
        SetMaxThruster(5);
    }

    private void Update()
    {
        if (!_isThrusting)
        {
            _slider.value += Time.deltaTime;
        }
        else
        {
            _slider.value -= Time.deltaTime * _regenerateRate;
            if (_slider.value <= 0)
            {
                _isThrusting = false;
            }
        }
    }

    public void ActivateThruster()
    {
        if (_slider.value >= _slider.maxValue)
        {
            _isThrusting = true;
        }
    }

    private void SetMaxThruster(int amount)
    {
        _slider.maxValue = amount;
        _slider.value = amount;
    }

    public bool GetIsThrusting()
    {
        return _isThrusting;
    }
}
