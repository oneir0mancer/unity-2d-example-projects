using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _thresholdDistance;
    
    private void Update()
    {
        Vector3 position = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - position;

        if (direction.sqrMagnitude < _thresholdDistance * _thresholdDistance) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _rotationSpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(position, mousePos, _movementSpeed * Time.deltaTime);
    }
}
