using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    //start pool size
    [SerializeField] private int _amountToPool;
    //allows pooler to expand list if overflown
    [SerializeField] private bool _shouldExpand;


    private List<IPooledObject> _pooledObjects;


    private void Start()
    {
        _pooledObjects = new List<IPooledObject>();

        for (int i = 0; i < _amountToPool; i++)
        {
            if (CreateNewObject() == null)
            {
                return;
            }
        }
    }


    //gets first inactive object or creates new if cant find and _shouldExpand
    public IPooledObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].IsActive)
            {
                return _pooledObjects[i];
            }
        }
        if (_shouldExpand)
        {
            return CreateNewObject();
        }
        return null;
    }


    private IPooledObject CreateNewObject()
    {
        if (!_prefab)
        {
            Debug.LogError("Prefab is null!");
            return null;
        }

        if (!_prefab.TryGetComponent(out IPooledObject pooledObject))
        {
            Debug.LogError("Prefab is not IPooledObject!");
            return null;
        }

        var newPojectile = Instantiate(_prefab, transform);
        newPojectile.gameObject.SetActive(false);
        pooledObject = newPojectile.GetComponent<IPooledObject>();
        _pooledObjects.Add(pooledObject);
        return pooledObject;
    }
}