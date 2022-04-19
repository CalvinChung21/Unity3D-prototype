using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour {

	public enum PathFindingMode{
		BreathFirstSearch,
		GreedyBestFirst,
		Dijkstra,
		Astar
	}

	public PathFindingMode pathFindingMode = PathFindingMode.BreathFirstSearch;

	PathRequestManager requestManager;
	Grid grid;
	
	void Awake() {
		requestManager = GetComponent<PathRequestManager>();
		grid = GetComponent<Grid>();
	}
	
	// public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
	// 	switch (pathFindingMode)
	// 		{
	// 			case PathFindingMode.BreathFirstSearch: 
	// 				StartCoroutine(BreathFirstSearch(startPos,targetPos));
	// 				break;
	// 			case PathFindingMode.GreedyBestFirst :
	// 				StartCoroutine(GreedyBestFirst(startPos,targetPos));
	// 				break;
	// 			case PathFindingMode.Dijkstra : 
	// 				StartCoroutine(Dijkstra(startPos,targetPos));
	// 				break;
	// 			case PathFindingMode.Astar : 
	// 				StartCoroutine(Astar(startPos,targetPos));
	// 				break;
	// 			default: break;
	// 		}
	// 	
	// }
	//
	// IEnumerator Astar(Vector3 startPos, Vector3 targetPos) {
	//
	// 	Vector3[] waypoints = new Vector3[0];
	// 	bool pathSuccess = false;
	// 	
	// 	Node startNode = grid.NodeFromWorldPoint(startPos);
	// 	Node targetNode = grid.NodeFromWorldPoint(targetPos);
	// 	
	// 	
	// 	if (startNode.walkable && targetNode.walkable) {
	// 		Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
	// 		HashSet<Node> closedSet = new HashSet<Node>();
	// 		openSet.Add(startNode);
	// 		
	// 		while (openSet.Count > 0) {
	// 			Node currentNode = openSet.RemoveFirst();
	// 			closedSet.Add(currentNode);
	// 			
	// 			if (currentNode == targetNode) {
	// 				pathSuccess = true;
	// 				break;
	// 			}
	// 			
	// 			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
	// 				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
	// 					continue;
	// 				}
	// 				
	// 				int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
	// 				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
	// 					neighbour.gCost = newMovementCostToNeighbour;
	// 					neighbour.hCost = GetDistance(neighbour, targetNode);
	// 					neighbour.parent = currentNode;
	// 					
	// 					if (!openSet.Contains(neighbour))
	// 					{
	// 						openSet.Add(neighbour);
	// 					}
	// 					else // or update the neighbour if it already exists
	// 					{
	// 						openSet.UpdateItem(neighbour);
	// 					}
	// 				}
	// 			}
	// 		}
	// 	}
	// 	yield return null;
	// 	if (pathSuccess) {
	// 		waypoints = RetracePath(startNode,targetNode);
	// 	}
	// 	requestManager.FinishedProcessingPath(waypoints,pathSuccess);
	// 	
	// }
	//
	// IEnumerator Dijkstra(Vector3 startPos, Vector3 targetPos)
	// {
	//
	// 	Vector3[] waypoints = new Vector3[0];
	// 	bool pathSuccess = false;
	//
	// 	Node startNode = grid.NodeFromWorldPoint(startPos);
	// 	Node targetNode = grid.NodeFromWorldPoint(targetPos);
	//
	//
	// 	if (startNode.walkable && targetNode.walkable)
	// 	{
	// 		Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
	// 		HashSet<Node> closedSet = new HashSet<Node>();
	// 		openSet.Add(startNode);
	//
	// 		while (openSet.Count > 0)
	// 		{
	// 			// visit the first node in the openset and remove it
	// 			Node currentNode = openSet.RemoveFirst();
	// 			// add it to close set as a visited node
	// 			closedSet.Add(currentNode);
	// 			// when the target node is found break the function
	// 			if (currentNode == targetNode)
	// 			{
	// 				pathSuccess = true;
	// 				break;
	// 			}
	//
	// 			// loop through each neighbour node
	// 			foreach (Node neighbour in grid.GetNeighbours(currentNode))
	// 			{
	// 				// if it is not walkable or visited, skip it
	// 				if (!neighbour.walkable || closedSet.Contains(neighbour))
	// 				{
	// 					continue;
	// 				}
	//
	// 				// calculate the cost
	// 				int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
	// 				// if the new g cost is less than the current neighbour g cost
	// 				// or the neighbour isn't in the openset
	// 				// then set the neighbour's g cost and parent
	// 				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
	// 				{
	// 					neighbour.gCost = newMovementCostToNeighbour;
	// 					neighbour.parent = currentNode;
	// 					// add the neighbour in openset
	// 					if (!openSet.Contains(neighbour))
	// 					{
	// 						openSet.Add(neighbour);
	// 					}
	// 					else // or update the neighbour if it already exists
	// 					{
	// 						openSet.UpdateItem(neighbour);
	// 					}
	// 				}
	// 			}
	// 		}
	//
	// 		yield return null;
	// 		if (pathSuccess)
	// 		{
	// 			waypoints = RetracePath(startNode, targetNode);
	// 		}
	//
	// 		requestManager.FinishedProcessingPath(waypoints, pathSuccess);
	//
	// 	}
	// }
	//
	// IEnumerator GreedyBestFirst(Vector3 startPos, Vector3 targetPos)
	// {
	//
	// 	Vector3[] waypoints = new Vector3[0];
	// 	bool pathSuccess = false;
	//
	// 	Node startNode = grid.NodeFromWorldPoint(startPos);
	// 	Node targetNode = grid.NodeFromWorldPoint(targetPos);
	//
	//
	// 	if (startNode.walkable && targetNode.walkable)
	// 	{
	// 		Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
	// 		HashSet<Node> closedSet = new HashSet<Node>();
	// 		openSet.Add(startNode);
	// 		
	// 		while (openSet.Count > 0) {
	// 			// visit the first node in the openset and remove it
	// 			Node currentNode = openSet.RemoveFirst();
	// 			// add it to close set as a visited node
	// 			closedSet.Add(currentNode);
	// 			// when the target node is found break the function
	// 			if (currentNode == targetNode) {
	// 				pathSuccess = true;
	// 				break;
	// 			}
	// 			// loop through each neighbour node
	// 			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
	// 				// if it is not walkable or visited, skip it
	// 				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
	// 					continue;
	// 				}
	// 				// calculate the cost
	// 				int newMovementCostToNeighbour = currentNode.hCost + GetDistance(currentNode, neighbour);	
	// 				// if the new g cost is less than the current neighbour g cost
	// 				// or the neighbour isn't in the openset
	// 				// then set the neighbour's g cost and parent
	// 				if (newMovementCostToNeighbour < neighbour.hCost || !openSet.Contains(neighbour)) {
	// 					neighbour.hCost = newMovementCostToNeighbour;
	// 					neighbour.parent = currentNode;
	// 					// add the neighbour in openset
	// 					if (!openSet.Contains(neighbour))
	// 					{
	// 						openSet.Add(neighbour);
	// 					}
	// 					else // or update the neighbour if it already exists
	// 					{
	// 						openSet.UpdateItem(neighbour);
	// 					}
	// 				}
	// 			}
	// 		}
	//
	// 		yield return null;
	// 		if (pathSuccess)
	// 		{
	// 			waypoints = RetracePath(startNode, targetNode);
	// 		}
	//
	// 		requestManager.FinishedProcessingPath(waypoints, pathSuccess);
	//
	// 	}
	// }
	//
	// IEnumerator BreathFirstSearch(Vector3 startPos, Vector3 targetPos)
	// {
	// 	int exploredNode = 0;
	// 	Vector3[] waypoints = new Vector3[0];
	// 	bool pathSuccess = false;
	//
	// 	Node startNode = grid.NodeFromWorldPoint(startPos);
	// 	Node targetNode = grid.NodeFromWorldPoint(targetPos);
	//
	//
	// 	if (startNode.walkable && targetNode.walkable)
	// 	{
	// 		Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
	// 		HashSet<Node> closedSet = new HashSet<Node>();
	// 		openSet.Add(startNode);
	// 		
	// 		while (openSet.Count > 0) {
	// 			// visit the first node in the openset and remove it
	// 			Node currentNode = openSet.RemoveFirst();
	//
	// 			// add it to close set as a visited node
	// 			closedSet.Add(currentNode);
	// 			// when the target node is found break the function
	// 			if (currentNode == targetNode) {
	// 				pathSuccess = true;
	// 				break;
	// 			}
	// 			// loop through each neighbour node
	// 			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
	// 				// if it is not walkable or visited, skip it
	// 				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
	// 					continue;
	// 				}
	// 				// add the neighbour in openset
	// 				if (!openSet.Contains(neighbour))
	// 				{
	// 					exploredNode++;
	// 					neighbour.exploredNodes = exploredNode;
	// 					neighbour.parent = currentNode;
	// 					openSet.Add(neighbour);
	// 				}
	// 				else // or update the neighbour if it already exists
	// 				{
	// 					openSet.UpdateItem(neighbour);
	// 				}
	// 			}
	// 		}
	//
	// 		yield return null;
	// 		if (pathSuccess)
	// 		{
	// 			waypoints = RetracePath(startNode, targetNode);
	// 		}
	//
	// 		requestManager.FinishedProcessingPath(waypoints, pathSuccess);
	//
	// 	}
	// }

// // with multithreading
	public void FindPath(PathRequest request, Action<PathResult> callback) 
	{
		// keep doing the find path method
		switch (pathFindingMode)
		{
			case PathFindingMode.BreathFirstSearch: 
				BreadthFirstSearchFindPath(request, callback);
				break;
			case PathFindingMode.GreedyBestFirst :
				GreedyBestFirstFindPath(request, callback); 
				break;
			case PathFindingMode.Dijkstra : 
				DijkstraFindPath(request, callback);
				break;
			case PathFindingMode.Astar : 
				AstarFindPath(request, callback);
				break;
			default: break;
		}
	}
	
	// A* pathfinding
	public void AstarFindPath(PathRequest request, Action<PathResult> callback)
	{
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(request.pathStart);
		Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);
		
		// check if start and target node are walkable
		if (startNode.walkable && targetNode.walkable)
		{
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				// visit the first node in the openset and remove it
				Node currentNode = openSet.RemoveFirst();
				// add it to close set as a visited node
				closedSet.Add(currentNode);
				// when the target node is found break the function
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				// loop through each neighbour node
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					// if it is not walkable or visited, skip it
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}
					// calculate the cost
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);	
					// if the new g cost is less than the current neighbour g cost
					// or the neighbour isn't in the openset
					// then set the neighbour's g cost, h cost and parent
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;
						// add the neighbour in openset
						if (!openSet.Contains(neighbour))
						{
							openSet.Add(neighbour);
						}
						else // or update the neighbour if it already exists
						{
							openSet.UpdateItem(neighbour);
						}
					}
				}
			}
		}
		// if found the goal, then retrace the path
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
			pathSuccess = waypoints.Length > 0;
		}
		// call the finished path finding process
		callback(new PathResult(waypoints, pathSuccess, request.callback));
	}
	
	public void DijkstraFindPath(PathRequest request, Action<PathResult> callback)
	{
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(request.pathStart);
		Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);
		
		// check if start and target node are walkable
		if (startNode.walkable && targetNode.walkable)
		{
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				// visit the first node in the openset and remove it
				Node currentNode = openSet.RemoveFirst();
				// add it to close set as a visited node
				closedSet.Add(currentNode);
				// when the target node is found break the function
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				// loop through each neighbour node
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					// if it is not walkable or visited, skip it
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}
					// calculate the cost
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);	
					// if the new g cost is less than the current neighbour g cost
					// or the neighbour isn't in the openset
					// then set the neighbour's g cost and parent
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.parent = currentNode;
						// add the neighbour in openset
						if (!openSet.Contains(neighbour))
						{
							openSet.Add(neighbour);
						}
						else // or update the neighbour if it already exists
						{
							openSet.UpdateItem(neighbour);
						}
					}
				}
			}
		}
		// if found the goal, then retrace the path
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
			pathSuccess = waypoints.Length > 0;
		}
		// call the finished path finding process
		callback(new PathResult(waypoints, pathSuccess, request.callback));
	}
	
	public void GreedyBestFirstFindPath(PathRequest request, Action<PathResult> callback)
	{
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(request.pathStart);
		Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);
		
		// check if start and target node are walkable
		if (startNode.walkable && targetNode.walkable)
		{
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				// visit the first node in the openset and remove it
				Node currentNode = openSet.RemoveFirst();
				// add it to close set as a visited node
				closedSet.Add(currentNode);
				// when the target node is found break the function
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				// loop through each neighbour node
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					// if it is not walkable or visited, skip it
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}
					// calculate the cost
					int newMovementCostToNeighbour = currentNode.hCost + GetDistance(currentNode, neighbour);	
					// if the new h cost is less than the current neighbour h cost
					// or the neighbour isn't in the openset
					// then set the neighbour's h cost and parent
					if (newMovementCostToNeighbour < neighbour.hCost || !openSet.Contains(neighbour)) {
						neighbour.hCost = newMovementCostToNeighbour;
						neighbour.parent = currentNode;
						// add the neighbour in openset
						if (!openSet.Contains(neighbour))
						{
							openSet.Add(neighbour);
						}
						else // or update the neighbour if it already exists
						{
							openSet.UpdateItem(neighbour);
						}
					}
				}
			}
		}
		// if found the goal, then retrace the path
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
			pathSuccess = waypoints.Length > 0;
		}
		// call the finished path finding process
		callback(new PathResult(waypoints, pathSuccess, request.callback));
	}
	
	// search for all the directions
	// without calculating the so far cost
	// until the target is found
	public void BreadthFirstSearchFindPath(PathRequest request, Action<PathResult> callback)
	{
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
	
		int exploredNode = 0;
		Node startNode = grid.NodeFromWorldPoint(request.pathStart);
		Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);
		
		// check if start and target node are walkable
		if (startNode.walkable && targetNode.walkable)
		{
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				// visit the first node in the openset and remove it
				Node currentNode = openSet.RemoveFirst();
	
				// add it to close set as a visited node
				closedSet.Add(currentNode);
				// when the target node is found break the function
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				// loop through each neighbour node
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					// if it is not walkable or visited, skip it
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}
					exploredNode++;
					neighbour.exploredNodes = exploredNode;
					if (!openSet.Contains(neighbour))
					{
						neighbour.parent = currentNode;
						openSet.Add(neighbour);
					}
					else // or update the neighbour if it already exists
					{
						openSet.UpdateItem(neighbour);
					}
				}
			}
		}
		// if found the goal, then retrace the path
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
			pathSuccess = waypoints.Length > 0;
		}
		// call the finished path finding process
		callback(new PathResult(waypoints, pathSuccess, request.callback));
	}
	
	// can't implement it as it can't find the shortest path efficiently
	// DFS will not explore all direction in a equal depth
	// it will explore a single direction and fully explore that direction first before
	// moving to other directions
	// IEnumerator DepthFirstSearchFindPath(Vector3 startPos, Vector3 targetPos) 
	// {
	// 	Vector3[] waypoints = new Vector3[0];
	// 	bool pathSuccess = false;
	// 	
	// 	Node startNode = grid.NodeFromWorldPoint(startPos);
	// 	Node targetNode = grid.NodeFromWorldPoint(targetPos);
	// 	
	// 	// check if start and target node are walkable
	// 	if (startNode.walkable && targetNode.walkable)
	// 	{
	// 		Stack<Node> openSet = new Stack<Node>();
	// 		HashSet<Node> closedSet = new HashSet<Node>();
	// 		openSet.Push(startNode);
	// 		
	// 		while (openSet.Count > 0) {
	// 			// visit the first node in the openset and remove it
	// 			Node currentNode = openSet.Pop();
	//
	// 			// add it to close set as a visited node
	// 			closedSet.Add(currentNode);
	// 			// when the target node is found break the function
	// 			if (currentNode == targetNode) {
	// 				pathSuccess = true;
	// 				break;
	// 			}
	// 			// loop through each neighbour node
	// 			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
	// 				// if it is not walkable or visited, skip it
	// 				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
	// 					continue;
	// 				}
	//
	// 				if (!openSet.Contains(neighbour)) {
	// 					neighbour.parent = currentNode;
	// 					// add the neighbour in openset
	// 					openSet.Push(neighbour);
	// 				}
	// 			}
	// 		}
	// 	}
	// 	
	// 	yield return null;
	// 	// if found the goal, then retrace the path
	// 	if (pathSuccess) {
	// 		waypoints = RetracePath(startNode,targetNode);
	// 	}
	// 	// call the finished path finding process
	// 	requestManager.FinishedProcessingPath(waypoints,pathSuccess);
	// }
	
	
	Vector3[] RetracePath(Node startNode, Node endNode) {
		// using the parent from each node to retrace the path
		// starting from the goal node and check if the parent is the start node
		// if not then let the current parent node to be the next current node
		// and repeat until the start node is the parent
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		// only return non duplicate nodes
		Vector3[] waypoints = SimplifyPath(path);
		// reverse the way points as the path starts from end node
		// after reverse it will starts from start node
		Array.Reverse(waypoints);
		return waypoints;
	}
	
	// to make sure every node in the path is not duplicated and every move has make new movement
	Vector3[] SimplifyPath(List<Node> path) {
		
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		
		for (int i = 1; i < path.Count; i ++) {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			// if the direction has changed
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}
	
	// diagonal cost = 14
	// other straight line cost = 10
	// calculate the cost between two nodes
	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
		
		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
	
	
}