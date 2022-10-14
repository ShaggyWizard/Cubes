using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject, IProjectile
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDistance;


    private float _distance;


    public bool IsActive => gameObject.activeInHierarchy;

    public float Speed => _speed;
    public float MaxDistance => _maxDistance;


    void Update()
    {
        Vector3 direction = transform.rotation * Vector3.forward;
        float distance = _speed * Time.deltaTime;
        transform.position += direction * distance;
        _distance += distance;
        if (_distance >= _maxDistance || _speed == 0)
        {
            Kill();
        }
    }


    public void Spawn(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
        _distance = 0f;
    }
    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
    public void SetDistance(float newDistance)
    {
        _maxDistance = newDistance;
    }
}