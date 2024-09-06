using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roads = new List<GameObject>();
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private float moveSpeed;
    public static GameObject[] enemyGroupsPrefabs;

    private void Awake()
    {
        enemyGroupsPrefabs = Resources.LoadAll<GameObject>("Prefabs/EnemyGroups");
    }

    void Update()
    {
        if (!PlayerMovement.IsPlayerDie())
        {
            for (int i = 0; i < roads.Count; i++)
            {
                roads[i].transform.position = Vector3.Lerp(roads[i].transform.position, roads[i].transform.position + Vector3.back * moveSpeed, Time.deltaTime);
            }

            if (roads[1].transform.position.z <= 40.2)
            {
                Destroy(roads[0]);
                roads.RemoveAt(0);
                roads.Add(Instantiate(roadPrefab));
            }
        }
    }
}
