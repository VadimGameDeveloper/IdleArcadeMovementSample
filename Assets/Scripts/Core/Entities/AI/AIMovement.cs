using UnityEngine;

public class AIMovement : Movement
{
    [SerializeField] private float _collisionDetectionDistance = 2f;
    [SerializeField] private float _minSpeed = 3.5f;
    [SerializeField] private float _maxSpeed = 10f;
    private void Start()
    {
        _movementSpeed = Random.Range(_minSpeed, _maxSpeed);
        _input = transform.forward * _movementSpeed / _maxSpeed;
    }
    protected override void ChangeMovementInput()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        if (Physics.Raycast(ray, out hit, _collisionDetectionDistance))
        {
            _input = new Vector2(hit.normal.x, hit.normal.z);
        }
    }
}
