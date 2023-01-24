using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity = 3;

    private Camera _camera;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        _camera = Camera.main;

        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawbed = Instantiate(prefab, _container.transform);
            spawbed.SetActive(false);

            _pool.Add(spawbed);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    protected void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector2(0, 0.5f));

        foreach (var item in _pool)
        {
            if (item.activeSelf == true)
            {
                if (item.transform.position.x < disablePoint.x)
                    item.SetActive(false);
            }
        }
    }

    //protected void DisableObjectAbroadScreen()
    //{
    //    foreach (var item in _pool)
    //    {
    //        if (item.activeSelf == true)
    //        {
    //            Vector3 road = _camera.WorldToViewportPoint(item.transform.position);
    //            if (road.x < 0)
    //                item.SetActive(false);
    //        }
    //    }
    //}

    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }
}
