using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    public event Action<float> OnChangeMoveSpeed;
    public event Action<int> OnAttackCurrentWeapon;

    private const int WEAPON_INDEX_ONE_HAND = 12;
    private const int WEAPON_INDEX_NONE = 0;

    [SerializeField] private int _weaponDamage = 2;
    [SerializeField] private bool _isCanAttack;

    private Movement _movement;
    private List<Entity> _currentAtttackTargets = new();

    protected override void OnEnable()
    {
        base.OnEnable();
        _movement = GetComponent<Movement>();
        _movement.OnSpeedChanged += OnChangedMoveSpeed;
    }
    private void OnDisable()
    {
        _movement.OnSpeedChanged -= OnChangedMoveSpeed;
    }
    private void OnChangedMoveSpeed(float moveSpeed)
    {
        OnChangeMoveSpeed?.Invoke(moveSpeed);
    }
    private void StartAttack()
    {
        OnAttackCurrentWeapon?.Invoke(WEAPON_INDEX_ONE_HAND);
    }
    private void StopAttack()
    {
        OnAttackCurrentWeapon?.Invoke(WEAPON_INDEX_NONE);
    }
    public void AttackMelee()
    {
        foreach(var target in _currentAtttackTargets)
        {
            target.GetDamage(_weaponDamage);
        }
    }
    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
    }
    protected override void OnEndHealth()
    {
        base.OnEndHealth();
        _movement.IsCanMove = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isCanAttack && other.TryGetComponent<Entity>(out Entity target) && target.IsLive)
        {
            if (!_currentAtttackTargets.Contains(target))
            {
                _currentAtttackTargets.Add(target);
            }

            StartAttack();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity target))
        {
            if (_currentAtttackTargets.Contains(target))
            {
                _currentAtttackTargets.Remove(target);
            }
        }

        if(_currentAtttackTargets == null || _currentAtttackTargets.Count == 0)
        {
            StopAttack();
        }
    }
}
