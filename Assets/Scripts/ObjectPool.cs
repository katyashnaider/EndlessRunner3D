using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private int _capacity;
        [SerializeField] private List<GameObject> _prefabs;
       
        private List<GameObject> _pool = new List<GameObject>();

        private void Start()
        {
            GameObject spawnedObject;

            for (int i = 0; i < _capacity; i++)
            {
                spawnedObject = CreateRandomObject();
                Despawn(spawnedObject);
            }
        }

        public GameObject Spawn()
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

        public void Despawn(GameObject prefab)
        {
            prefab.SetActive(false);
            _pool.Add(prefab);
        }

        private GameObject CreateRandomObject()
        {
            return Instantiate(_prefabs[Random.Range(0, _prefabs.Count)]);
        }
    }
}
