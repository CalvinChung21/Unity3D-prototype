using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
   // whether we want to display the grid gizmos
   public bool displayGridGizmos;
   // get the position of the player
   public Transform player;
   // to determined which game objects are not walkable
   public LayerMask unwalkableMask;
   // for convenient of adjusting the size of the grid and the radious of the node inside the Unity Editor
   public Vector2 gridWorldSize;
   public float nodeRadius;

   // the size of each node and the size of the grid
   Node[,] grid;
   private float nodeDiameter;
   private int gridSizeX, gridSizeY;
   // to get the maximum number of the node
   public int MaxSize { get => gridSizeX * gridSizeY; }
   private void Awake()
   {
      // initialize the gird when awake
      nodeDiameter = nodeRadius * 2;
      gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
      gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
      CreateGrid();
   }
   
   void CreateGrid()
   {
      // assign memories to the grid
      grid = new Node[gridSizeX, gridSizeY];
      
      // the bottom left corner position of the world
      // the current game object's position - x axis of half of the grid world size and - z axis of half of the grid world size
      Vector3 worldBottomLeft =
         transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
      
      for (int x = 0; x < gridSizeX; x++)
      {
         for (int y = 0; y < gridSizeY; y++)
         {
            // the world point of each node
            Vector3 worldPoint = worldBottomLeft 
                                 + Vector3.right * (x * nodeDiameter + nodeRadius) 
                                 + Vector3.forward * (y * nodeDiameter + nodeRadius);
            // check is there a collision with a game object with unwalkable mask
            bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)); 
            
            grid[x, y] = new Node(walkable, worldPoint, x, y);
         }
      }
   }

   public List<Node> GetNeighbours(Node node)
   {
      List<Node> neighbours = new List<Node>();
      // getting the neighbours from four directions
      // originalX-1, originalY-1 <-- top left
      // originalX-1, originalY   <-- left
      // originalX-1, originalY+1 <-- bottom left
      // originalX, originalY-1   <-- top
      // originalX, originalY     <-- skip this
      // originalX, originalY+1   <-- bottom 
      // originalX+1, originalY-1 <-- top right 
      // originalX+1, originalY   <-- right
      // originalX+1, originalY+1 <-- bottom right
      for (int x = -1; x <= 1; x++)
      {
         for (int y = -1; y <= 1; y++)
         {
            // skip the 0, 0
            if(x == 0 && y == 0)
               continue;

            int checkX = node.gridX + x;
            int checkY = node.gridY + y;
            // check if the position is within the boundaries
            if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
            {
               neighbours.Add(grid[checkX, checkY]);
            }
         }
      }
      return neighbours;
   }
   
   public Node NodeFromWorldPoint(Vector3 worldPosition)
   {
      // mapping the position from world point to the original x and original y in the grid array
      float percentX = (worldPosition.x + gridWorldSize.x/2)/gridWorldSize.x;
      float percentY = (worldPosition.z + gridWorldSize.y/2)/gridWorldSize.y;
      percentX = Mathf.Clamp01(percentX);
      percentY = Mathf.Clamp01(percentY);

      int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
      int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
      return grid[x, y];
   }
   
   // Unity build-in function
   // draw the 2d grid on the unity 3D world
   // for visualization
   // private void OnDrawGizmos()
   // {
   //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
   //
   //    if (grid != null && displayGridGizmos)
   //       {
   //          foreach (Node n in grid)
   //          {
   //             Gizmos.color = (n.walkable) ? Color.white : Color.red;
   //             Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
   //          }
   //       }
   // }
}
