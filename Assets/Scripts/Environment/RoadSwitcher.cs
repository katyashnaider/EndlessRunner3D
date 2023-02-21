using System.Collections;
using UnityEngine;

public class RoadSwitcher : MonoBehaviour
{
    [SerializeField] private Transform[] _roadPoints;
    [SerializeField] private float _speed = 1f;

    public IEnumerator ChangeRoad(Transform playerPosition, int index)
    {
        float x = playerPosition.position.x;
        float target = _roadPoints[index].position.x;

        while (playerPosition.position.x != target)
        {
            x = Mathf.MoveTowards(playerPosition.position.x, target, _speed * Time.deltaTime);
            Vector3 newPlayerPosition = new Vector3(x, playerPosition.position.y, playerPosition.position.z);
            playerPosition.position = newPlayerPosition;

            yield return null;
        }
    }
}