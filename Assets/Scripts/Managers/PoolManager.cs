using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private CharacterDB _characterDB;
    
    private Dictionary<Type, GameObject> _prefabs = new Dictionary<Type, GameObject>();
    private Dictionary<Type, IObjectPool<GameObject>> _pools = new Dictionary<Type, IObjectPool<GameObject>>();

    public void Init()
    {
        foreach (var prefab in _characterDB.characterPrefabs)
        {
            var component = prefab.GetComponent<ACharacter>();
            if (component != null)
            {
                _prefabs[component.GetType()] = prefab;
            }
        }
    }

    private IObjectPool<GameObject> CreatePool(GameObject argPrefab)
    {
        return new ObjectPool<GameObject>(
            createFunc: () => Instantiate(argPrefab),
            actionOnGet: OnPoolGet,
            actionOnRelease: OnPoolRelease,
            actionOnDestroy: OnPoolDestroy
        );
    }

    private void OnPoolGet(GameObject argObject)
    {
        if (argObject == null)
            return;
        argObject.SetActive(true);
    }
    
    private void OnPoolRelease(GameObject argObject)
    {
        if (argObject == null)
            return;
        argObject.SetActive(false);
    }
    
    private void OnPoolDestroy(GameObject argObject)
    {
        if (argObject == null)
            return;
        Destroy(argObject);
    }
    
    public T Spawn<T>(Vector3 argPosition) where T : Component
    {
        Type type = typeof(T);
        if (!_pools.ContainsKey(type))
        {
            if (!_prefabs.TryGetValue(type, out GameObject prefab))
            {
                Debug.LogError($"{type.Name} does not exist");
                return null;
            }
            _pools[type] = CreatePool(prefab);
        }

        GameObject go = _pools[type].Get();
        if (go == null)
            return null;
        
        go.transform.position = argPosition;
        return go.GetComponent<T>();
    }

    public void Release<T>(T argObj) where T : Component
    {
        if (argObj == null || argObj.gameObject == null) return;

        Type type = typeof(T);
        if (_pools.TryGetValue(typeof(T), out var pool))
        {
            pool.Release(argObj.gameObject);
        }
        else
        {
            Debug.LogError($"{type.Name} does not exist");
            Destroy(argObj.gameObject);
        }
    }
}
