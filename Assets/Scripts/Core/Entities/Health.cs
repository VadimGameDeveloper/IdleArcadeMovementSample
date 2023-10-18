using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnEndHeath;

    [SerializeField] protected int _maxHealth;
    [SerializeField] protected HealthBar _healthbar;

    private int _currentHealth;

    public int CurrentHealth => _currentHealth;

    protected void Awake()
    {
        SetStartHealth();
    }
    public virtual void ChangeHealthValueBy(int changingValue)
    {
        _currentHealth += changingValue;
        _currentHealth = _currentHealth > _maxHealth ? _maxHealth : _currentHealth;

        UpdateHealthBar(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            OnEndHeath?.Invoke();
        }
    }

    protected void UpdateHealthBar(int currentHP, int maxHP)
    {
        if(_healthbar == null)
        {
            return;
        }

        _healthbar.gameObject.SetActive(true);

        _healthbar.SetValue((float)currentHP, (float)maxHP);
    }

    public void SetStartHealth()
    {
        _currentHealth = _maxHealth;
       // UpdateHealthBar(_currentHealth, _maxHealth);
    }
}
