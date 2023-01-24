using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoolManager : MonoBehaviour
{
    private class Pool
    {
        private List<GameObject> _inactive = new List<GameObject>();
        private GameObject _prefab;

        public Pool(GameObject prefab)
        {
            this._prefab = prefab;
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject prefab;

            if (_inactive.Count == 0)
            {
                prefab = Instantiate(_prefab, position, rotation);
                prefab.name = _prefab.name;
                //prefab.transform.SetParent(Instance.transform);
            }
            else
            {
                prefab = _inactive[_inactive.Count - 1];
                _inactive.RemoveAt(_inactive.Count - 1);
            }

            prefab.transform.position = position;
            prefab.transform.rotation = rotation;
            prefab.SetActive(true);

            return prefab;
        }

        public void Despawn(GameObject prefab)
        {
            prefab.SetActive(false);
            _inactive.Add(prefab);
        }
    }

    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Init(prefab);
        return _pools[prefab.name].Spawn(position, rotation);
    }

    public GameObject Spawn(GameObject[] prefabs, Vector3 position, Quaternion rotation)
    {
        foreach (GameObject prefab in prefabs)
        {
            Init(prefab);
            return _pools[prefab.name].Spawn(position, rotation);
        }

        return null;
    }

    public void Despawn(GameObject prefab)
    {
        if (_pools.ContainsKey(prefab.name))
            _pools[prefab.name].Despawn(prefab);
        else
            Destroy(prefab);
    }

    public void Despawn(GameObject[] prefabs)
    {
        foreach (GameObject prefab in prefabs)
        {
            if (_pools.ContainsKey(prefab.name))
                _pools[prefab.name].Despawn(prefab);
            else
                Destroy(prefab);
        }
    }

    public void Prelooad(GameObject prefab, int count)
    {
        Init(prefab);

        GameObject[] prefabs = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            prefabs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
        }

        for (int i = 0; i < count; i++)
        {
            Despawn(prefabs[i]);
        }
    }

    public void Prelooad(GameObject[] prefabs, int count)
    {
        foreach (GameObject prefab in prefabs)
        {
            Init(prefab);
        }

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            prefabs[randomIndex] = Spawn(prefabs, Vector3.zero, Quaternion.identity);
        }

        for (int i = 0; i < count; i++)
        {
            Despawn(prefabs[i]);
        }
    }

    private void Init(GameObject prefab)
    {
        if (prefab != null && _pools.ContainsKey(prefab.name) == false)
            _pools[prefab.name] = new Pool(prefab);
    }
}
