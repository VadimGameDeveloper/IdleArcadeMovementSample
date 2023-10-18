using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Entity : MonoBehaviour
{
    public event Action OnDeath;
    public event Action OnGetDamage;

    protected Health _health;

    public bool IsLive => _health.CurrentHealth > 0;

    protected virtual void OnEnable()
    {
        _health = GetComponent<Health>();
        _health.OnEndHeath += OnEndHealth;
    }
    private void OnDisable()
    {
        if (_health != null && IsLive)
        {
            _health.OnEndHeath -= OnEndHealth;
        }
    }
    public virtual void GetDamage(int damage)
    {
        if (!IsLive)
        {
            return;
        }
        _health.ChangeHealthValueBy(-damage);
        OnGetDamage?.Invoke();
    }
    protected virtual void OnEndHealth()
    {
        if (_health != null)
        {
            _health.OnEndHeath -= OnEndHealth;
        }
        OnDeath?.Invoke();
    }
}
