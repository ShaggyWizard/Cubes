using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPooler _pooler;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _cubeSpeed;
    [SerializeField] private float _cubeDistance;


    private float _lastSpawnTime;


    private void Update()
    {
        _lastSpawnTime += Time.deltaTime;
        if (_lastSpawnTime > _spawnRate)
        {
            _lastSpawnTime -= _spawnRate;
            var pooledObject = _pooler.GetPooledObject();

            if (pooledObject != null)
            {
                pooledObject.Spawn(transform.position, transform.rotation);

                if (pooledObject is IProjectile)
                {
                    IProjectile projectile = pooledObject as IProjectile;

                    projectile.SetDistance(_cubeDistance);
                    projectile.SetSpeed(_cubeSpeed);
                }
            }
        }
    }
}
