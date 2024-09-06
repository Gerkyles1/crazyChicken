using UnityEngine;

public class RoadControler : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField, Range(0.1f, 1)] private float obstacleSpavnChance = 0.3f;
    [SerializeField, Range(0.1f, 1)] private float enemySpavnChance = 0.7f;
    private const int ROAD_PARTS = 9;
    void Start()
    {
        for (int i = 0; i < ROAD_PARTS; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (Random.value < obstacleSpavnChance)
                {
                    GameObject obstacleInstance = Instantiate(obstaclePrefab);
                    obstacleInstance.transform.parent = transform;
                    obstacleInstance.transform.localPosition = new Vector3((j * 3.5f), 1.5f, i - 5);
                }
                else if (Random.value < enemySpavnChance)
                {
                    GameObject enemygr = Instantiate(RoadCreator.enemyGroupsPrefabs[Random.Range(0, RoadCreator.enemyGroupsPrefabs.Length)]);
                    enemygr.transform.parent = transform;
                    enemygr.transform.localPosition = new Vector3((j * 3.5f), 0, i - 5);


                }
            }
        }
    }

}
