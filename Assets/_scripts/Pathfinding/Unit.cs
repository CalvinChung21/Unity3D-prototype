using System;
using UnityEngine;
using System.Collections;
using CommandPattern;

public class Unit : MonoBehaviour {

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;
    
    private Transform target;
    public float speed = 5;
    private int targetIndex = 0;

    public float turnSpeed = 3;
    public float turnDst = 5;
    public float stoppingDst = 10;

    Path path;
    void Start() {
        StartCoroutine (UpdatePath ());
    }
    private void Awake()
    {
        // target = GameObject.Find("FlareThrew(Clone)")!=null
        //     ? GameObject.Find("FlareThrew(Clone)").transform
        //     : GameObject.Find("FPSController").transform;
    }
    
    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
        if (pathSuccessful) {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    void Update()
    {
        // pathfinding
        target = GameObject.Find("FlareThrew(Clone)")!=null
            ? GameObject.Find("FlareThrew(Clone)").transform
            : GameObject.Find("FPSController").transform;
        
        // if (gameObject.tag == "NPC" && !gameObject.GetComponent<Ghost>().stopPathFinding)
        // {
        //     if (target.hasChanged)
        //     {
        //         PathRequestManager.RequestPath(new PathRequest(transform.position,target.position, OnPathFound));
        //         target.hasChanged = false;
        //     }
        // }
        //
        // if (target.tag == "Glowstick")
        // {
        //     Patrol.patrolOn = false;
        // }
        // if (gameObject.tag != "NPC" &&!Patrol.patrolOn)
        // {
        //     if (target.hasChanged)
        //     {
        //         PathRequestManager.RequestPath(new PathRequest(transform.position,target.position, OnPathFound));
        //         target.hasChanged = false;
        //     }
        // }
        
    }
    
    IEnumerator UpdatePath() {

        if (Time.timeSinceLevelLoad < .3f) {
            yield return new WaitForSeconds (.3f);
        }
        PathRequestManager.RequestPath (new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true) {
            yield return new WaitForSeconds (minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
                PathRequestManager.RequestPath (new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath() {

        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt (path.lookPoints [0]);

        float speedPercent = 1;

        while (followingPath) {
            Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
            while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
                if (pathIndex == path.finishLineIndex) {
                    followingPath = false;
                    break;
                } else {
                    pathIndex++;
                }
            }

            if (followingPath) {

                if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
                    speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
                    if (speedPercent < 0.01f) {
                        followingPath = false;
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
            }

            yield return null;

        }
    }

    // public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
    //     if (pathSuccessful) {
    //         path = waypoints;
    //         StopCoroutine("FollowPath");
    //         StartCoroutine("FollowPath");
    //     }
    // }
    //
    // IEnumerator FollowPath() {
    //     // break the ienumerator when there is no path to follow
    //     if(path.Length == 0) yield break;
    //     
    //     Vector3 currentWaypoint = path[0];
    //
    //     while (true) {
    //         if (transform.position == currentWaypoint) {
    //             targetIndex++;
    //             // when the index is out of the range, then exit the coroutine
    //             if (targetIndex >= path.Length) { yield break; }
    //             
    //             currentWaypoint = path[targetIndex];
    //         }
    //         // rotate the game object 
    //         if (targetIndex != path.Length)
    //         {
    //             gameObject.transform.LookAt(currentWaypoint);
    //         }
    //
    //         transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
    //         yield return null;
    //     }
    // }
    //
    // // Drawing the line for the shortest path
    // public void OnDrawGizmos() {
    //     if (path != null) {
    //         for (int i = targetIndex; i < path.Length; i ++) {
    //             Gizmos.color = Color.white;
    //             Gizmos.DrawCube(path[i], Vector3.one);
    //
    //             if (i == targetIndex) {
    //                 Gizmos.DrawLine(transform.position, path[i]);
    //             }
    //             else {
    //                 Gizmos.DrawLine(path[i-1],path[i]);
    //             }
    //         }
    //     }
    // }
}