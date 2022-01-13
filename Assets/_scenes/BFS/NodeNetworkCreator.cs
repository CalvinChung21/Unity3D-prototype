using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeNetworkCreator : MonoBehaviour
{
    public int boardWidth = 10;
    public int boardHeight = 15;
    public IDictionary<Vector3, bool> walkablePositions = new Dictionary<Vector3, bool>();
    public IDictionary<Vector3, GameObject> nodeReference = new Dictionary<Vector3, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InitializeNodeNetwork(40);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeNodeNetwork(int numObstacles)
    {
        var node = GameObject.Find("Node");
        var obstacle = GameObject.Find("Obstacles");
        var width = boardWidth;
        var height = boardHeight;
        
        GameObject goal = GameObject.Find("Goal");
        walkablePositions.Add(new KeyValuePair<Vector3, bool>(goal.transform.localPosition, true));
        nodeReference.Add(new KeyValuePair<Vector3, GameObject>(goal.transform.localPosition, goal));

        List<Vector3> obstacles = GenerateObstacles(numObstacles);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 newPosition = new Vector3(i, 0, j);
                GameObject copy;
                if (obstacles.Contains(newPosition))
                {
                    copy = Instantiate(obstacle);
                    copy.transform.position = newPosition;
                    walkablePositions.Add(new KeyValuePair<Vector3, bool>(newPosition, false));
                }
                else
                {
                    copy = Instantiate(node);
                    copy.transform.position = newPosition;
                    walkablePositions.Add(new KeyValuePair<Vector3, bool>(newPosition, true));
                }

                nodeReference.Add(newPosition, copy);
            }
        }
    }

    List<Vector3> GenerateObstacles(int num)
    {
        List<Vector3> obstacles = new List<Vector3>();

        for (int i = 0; i < num; i++)
        {
            Vector3 nodePosition = new Vector3(Random.Range(0, boardWidth - 1), 0, Random.Range(0, boardHeight - 1));
            obstacles.Add(nodePosition);
        }

        return obstacles;
    }
    
}
