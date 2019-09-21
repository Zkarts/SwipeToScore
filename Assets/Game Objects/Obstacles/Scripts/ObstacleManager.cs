using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {

    [SerializeField]
    private ObstacleBase boxObstacle, cylinderObstacle;

    [SerializeField]
    private float minDistance = 2f, maxDistance = 8.5f;

    [SerializeField]
    private float fieldWidth = 4.5f;

    [SerializeField]
    private float minDistanceBetweenObstacles = 1.5f;

    private List<ObstacleBase> currentObstacles = new List<ObstacleBase>();

    public void GenerateObstacles(int level) {
        int amountOfObstacles = Mathf.Min(4, level / 5);

        for (int i = 0; i < amountOfObstacles; i++) {
            GenerateObstacle();
        }
    }

    public void ResetObstacles() {
        foreach (ObstacleBase obstacle in currentObstacles) {
            //the obstacles can already have been destroyed through gameplay
            if (obstacle != null) {
                Destroy(obstacle.gameObject);
            }
        }
        currentObstacles.Clear();
    }

    private void GenerateObstacle() {
        ObstacleBase newObstacle;
        int obstacleType = Random.Range(0, 2);

        if (obstacleType == 0) {
            newObstacle = Instantiate<ObstacleBase>(cylinderObstacle);
        }
        else {
            newObstacle = Instantiate<ObstacleBase>(boxObstacle);
        }

        float halfWidth = 0.5f * fieldWidth;

        while (true) {
            float posX = Random.Range(-halfWidth, halfWidth);
            float posZ = Random.Range(minDistance, maxDistance);
            newObstacle.transform.position = new Vector3(posX, 0, posZ);

            bool isTooClose = false;
            foreach (ObstacleBase obstacle in currentObstacles) {
                if (Vector3.Distance(obstacle.transform.position, newObstacle.transform.position) < minDistanceBetweenObstacles) {
                    isTooClose = true;
                    break;
                }
            }

            if (!isTooClose) {
                break;
            }
        }

        currentObstacles.Add(newObstacle);
    }

}
