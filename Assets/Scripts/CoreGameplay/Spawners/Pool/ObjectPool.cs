using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private List<GameObject> _prefabs;

    private List<GameObject> _pool = new List<GameObject>();
    private int _prefabIndex = 0;

    private void Start()
    {
        GameObject spawnedObject;

        for (int i = 0; i < _capacity; i++)
        {
            spawnedObject = CreateRandomObject();
            Put(spawnedObject);
        }
    }

    public GameObject Get()
    {
        GameObject spawnedObject;
        int poolCount = _pool.Count;

        if (poolCount == 0)
        {
            spawnedObject = CreateRandomObject();
        }
        else
        {
            spawnedObject = _pool[Random.Range(0, poolCount)];
            _pool.Remove(spawnedObject);
        }

        spawnedObject.SetActive(true);

        return spawnedObject;
    }

    public void Put(GameObject prefab)
    {
        prefab.SetActive(false);
        _pool.Add(prefab);
    }

    private GameObject GetNextPrefab()
    {
        int nextIndex = _prefabIndex + 1;

        if (nextIndex >= _prefabs.Count)
            nextIndex = 0;

        _prefabIndex = nextIndex;

        return _prefabs[nextIndex];
    }

    private GameObject CreateRandomObject()
    {
        //return Instantiate(_prefabs[Random.Range(0, _prefabs.Count)]);
        return Instantiate(GetNextPrefab());
    }
}
