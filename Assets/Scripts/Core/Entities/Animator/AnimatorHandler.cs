using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    protected Animator _animator;

    protected virtual void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    protected void SetTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
    protected void SetVariableInt(string name, int value)
    {
        _animator.SetInteger(name, value);
    }
    protected void SetVariableFloat(string name, float value)
    {
        _animator.SetFloat(name, value);
    }
    protected void SetVariableBool(string name, bool value)
    {
        _animator.SetBool(name, value);
    }
}
