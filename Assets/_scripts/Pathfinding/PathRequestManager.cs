using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class PathRequestManager : MonoBehaviour {

    // Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    // PathRequest currentPathRequest;
    //
    // static PathRequestManager instance;
    // Pathfinding pathfinding;
    //
    // bool isProcessingPath;
    //
    // void Awake() {
    //     instance = this;
    //     pathfinding = GetComponent<Pathfinding>();
    // }
    //
    // public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) {
    //     PathRequest newRequest = new PathRequest(pathStart,pathEnd,callback);
    //     instance.pathRequestQueue.Enqueue(newRequest);
    //     instance.TryProcessNext();
    // }
    //
    // void TryProcessNext() {
    //     if (!isProcessingPath && pathRequestQueue.Count > 0) {
    //         currentPathRequest = pathRequestQueue.Dequeue();
    //         isProcessingPath = true;
    //         pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
    //     }
    // }
    //
    // public void FinishedProcessingPath(Vector3[] path, bool success) {
    //     currentPathRequest.callback(path,success);
    //     isProcessingPath = false;
    //     TryProcessNext();
    // }
    //
    // struct PathRequest {
    //     public Vector3 pathStart;
    //     public Vector3 pathEnd;
    //     public Action<Vector3[], bool> callback;
    //
    //     public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback) {
    //         pathStart = _start;
    //         pathEnd = _end;
    //         callback = _callback;
    //     }
    //
    // }
    //
    // for multithreading
    Queue<PathResult> results = new Queue<PathResult>();
    
    static PathRequestManager instance;
    Pathfinding pathfinding;
    
    void Awake() {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }
    
    void Update() {
        if (results.Count > 0) {
            int itemsInQueue = results.Count;
            lock (results) {
                for (int i = 0; i < itemsInQueue; i++) {
                    PathResult result = results.Dequeue ();
                    result.callback (result.path, result.success);
                }
            }
        }
    }

    public static void RequestPath(PathRequest request) {
        ThreadStart threadStart = delegate {
            instance.pathfinding.FindPath (request, instance.FinishedProcessingPath);
        };
        threadStart.Invoke ();
    }
    
    public void FinishedProcessingPath(PathResult result) {
        // each time only one function can access the results
        lock (results) {
            results.Enqueue (result);
        }
    }
    
}

public struct PathResult {
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResult (Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }

}

public struct PathRequest {
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;

    public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback) {
        pathStart = _start;
        pathEnd = _end;
        callback = _callback;
    }

}