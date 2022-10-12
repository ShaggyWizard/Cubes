using UnityEngine;


public interface IPooledObject
{
    public bool IsActive { get; }


    public void Spawn(Vector3 position, Quaternion rotation);
    public void Kill();
}