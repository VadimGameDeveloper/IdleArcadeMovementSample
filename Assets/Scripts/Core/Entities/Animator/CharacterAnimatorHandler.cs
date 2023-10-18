using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Character))]
public class CharacterAnimatorHandler : AnimatorHandler
{
    private Character _character;

    protected override void OnEnable()
    {
        base.OnEnable();
        _character = GetComponent<Character>();

        _character.OnAttackCurrentWeapon += OnAttackCurrentWeapon;
        _character.OnChangeMoveSpeed += OnChangeMoveSpeed;
        _character.OnDeath += OnDeath;
        _character.OnGetDamage += OnGetDamage;
    }
    private void OnDisable()
    {
        _character.OnAttackCurrentWeapon -= OnAttackCurrentWeapon;
        _character.OnChangeMoveSpeed -= OnChangeMoveSpeed;
        _character.OnDeath -= OnDeath;
        _character.OnGetDamage -= OnGetDamage;
    }

    private void OnAttackCurrentWeapon(int weaponIndex)
    {
        SetVariableInt(AnimatorFieldsName.WeaponType_int, weaponIndex);
    }
    private void OnChangeMoveSpeed(float moveSpeed)
    {
        SetVariableFloat(AnimatorFieldsName.Speed_f, moveSpeed);
    }

    private void OnDeath()
    {
        SetVariableBool(AnimatorFieldsName.Death_b, true);
    }
    private void OnGetDamage()
    {
        SetTrigger(AnimatorFieldsName.GetHit);
    }
}
