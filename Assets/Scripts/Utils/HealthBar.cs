using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _filler;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _showingTime = 2;

    Coroutine _showingCoroutine;

    public void SetValue(float currentValue, float maxValue)
    {
        float value = currentValue / maxValue;

        if (_filler != null)
        {
            _filler.fillAmount = value;
        }

        if(_slider != null)
        {
            _slider.value = value;
        }

        if(_showingCoroutine != null)
        {
            StopCoroutine(_showingCoroutine);
        }

        if(_showingTime > 0)
        {
            _showingCoroutine = StartCoroutine(ShowingCoroutine(_showingTime));
        }
    }

    private IEnumerator ShowingCoroutine(float showingTime)
    {
        float currentTimer = 0f;
        while(currentTimer < showingTime)
        {
            currentTimer += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
