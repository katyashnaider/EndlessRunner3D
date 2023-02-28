using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    private Queue<T> _pool = new Queue<T>();

    public T Get()
    {
        T spawnedObject = _pool.Count == 0 ? CreateObject() : _pool.Dequeue();

        spawnedObject.gameObject.SetActive(true);

        return spawnedObject;
    }

    public void Put(T obj)
    {
        _pool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }

    protected abstract T CreateObject();
}