using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Agent : MonoBehaviour
{
    IDictionary<Vector3, Vector3> nodeParents = new Dictionary<Vector3, Vector3>();

    public IDictionary<Vector3, bool> walkablePositions;

    private NodeNetworkCreator nodeNetwork;

    private IList<Vector3> path;

    bool moveCube = false;

    int i; 
    // Start is called before the first frame update
    void Start()
    {
        nodeNetwork = GameObject.Find("NodeNetwork").GetComponent<NodeNetworkCreator>();
        walkablePositions = nodeNetwork.walkablePositions;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCube)
        {
            float step = Time.deltaTime * 5;
            transform.position = Vector3.MoveTowards(transform.position, path[i], step);
            if (transform.position.Equals(path[i]) && i >= 0)
                i--;
            if (i < 0)
                moveCube = false;
        }
    }

    bool CanMove(Vector3 nextPosition)
    {
        return (walkablePositions.ContainsKey(nextPosition) ? walkablePositions[nextPosition] : false);
    }

    // public void DisplayShortestPath(bool isDFS)
    // {
    //     nodeParents = new Dictionary<Vector3, Vector3>();
    //     path = FindShortestPath(isDFS);
    //
    //     Sprite exploredTile = Resources.Load<Sprite>("path 1");
    //     Sprite victoryTile = Resources.Load<Sprite>("victory 1");
    //
    //     foreach (Vector3 node in path)
    //     {
    //         if (isDFS)
    //         {
    //             nodeNetwork.nodeReference[node].GetComponent<SpriteRenderer>().sprite = victoryTile;
    //         }
    //         else
    //         {
    //             
    //         }
    //     }
    // }

    // IList<Vector3> FindShortestPath(bool isDFS = false)
    // {
    //     IList<Vector3> path = new List<Vector3>();
    //     Vector3 goal;
    //     if (isDFS)
    //     {
    //         goal = FindShortestPathDFS(this.transform.localPosition, GameObject.Find("Goal").transform.localPosition);
    //     }
    //     else
    //     {
    //         goal = FindShortestPathBFS(this.transform.localPosition, GameObject.Find("Goal").transform.localPosition);
    //     }
    //
    //     if (goal == this.transform.localPosition)
    //     {
    //         return null;
    //     }
    // }
}
