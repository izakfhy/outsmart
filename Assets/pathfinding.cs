using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/* 
 * A* pathfinding class
   Note that this is my implementation of A*,
 * this may not be the most efficient pathfinding class.
 * I wil optimize if i I have the time and when there is a need for it.
 * Also, this pathfinding is customised for our grid system. thus the grid system it uses
 * comes from the gridscript wherebby the grid is a 2D array of cells(custom class object).
 * usually the generic pathfinding algo uses 2D array of integers.
 */


public class pathfinding  {

    //public List<cell> m_openList;
    //public List<cell> m_closedList;
    public Dictionary<cell, cell> m_openList;
    public List<cell> m_closedList;
    public Vector2 m_from; // note that this is grid integer values only, not floats!
    public Vector2 m_to;
    public cell[,] m_grid;

    public pathfinding(cell[,] grid)
    {
        m_grid = grid;
        m_openList = new Dictionary<cell, cell>();
        m_closedList = new List<cell>();
    }
    // Use this for initialization
    void Start()
    {
   
	}
	
    public void PathPoints(Vector2 from, Vector2 to)
    {
        m_to = to;
        m_from = from;
    }

    public List<cell> FindPath(cell from, cell to)
    {
        
        //add initial grid pos
        var initFVal = from.m_G + from.m_H;

        int xbound = m_grid.GetUpperBound(0);
        int ybound = m_grid.GetUpperBound(1);

        m_openList.Add(from, from);

        do
        {
            var curSq = m_openList.Keys.Min();
            var c = m_openList[curSq];

            m_closedList.Add(c);
            m_openList.Remove(c);

            var found = m_closedList.FirstOrDefault(t => t.m_x == (int)to.m_x && t.m_y == (int)to.m_y);
            
            //found the end point in closelist, found path
            if (!object.ReferenceEquals(found, null))
            {
                return Backtrack(from, found);
            }

            List<cell> adjacentsq = new List<cell>();
            //find adjacentsq
            for(int i =c.m_x - 1; i <= c.m_x + 1; i++)
            {
                for(int j = c.m_y - 1; j <= c.m_y + 1; j++)
                {
                    if (i < 0 || i > xbound)
                        continue;
                    if (j < 0 || j > ybound)
                        continue;
                    var val = m_grid[i, j].m_val;
                    if (val == 0)
                    {
                        adjacentsq.Add(m_grid[i,j]);
                    }
                }
            }

            foreach(var sq in adjacentsq)
            {
                var inclose = m_closedList.FirstOrDefault(t => t.m_x == (int)sq.m_x && t.m_y == (int)sq.m_y);
                if (!object.ReferenceEquals(inclose, null))
                    continue;

                var inopen = m_openList.FirstOrDefault(t => t.Value.m_x == (int)sq.m_x && t.Value.m_y == (int)sq.m_y);
                int newGCost = (int)c.m_G + GetGDist(c, sq);
                float newHCost = CalculateHeuristic(sq, to);

                if (object.ReferenceEquals(inopen.Value, null) || newGCost < sq.m_G)
                {
                    sq.m_G = newGCost;
                    sq.m_H = newHCost;
                    sq.m_parent = c;
                    if (!object.ReferenceEquals(inopen.Value, null))
                    {
                        inopen.Value.m_G = newGCost;
                        inopen.Value.m_H = newHCost;
                        inopen.Value.m_parent = c;
                    }
                    else
                        m_openList.Add(sq, sq);
                }
            
            }

        } while (m_openList.Count() > 0);

        return null;
    }

    private static List<cell> Backtrack(cell startNode, cell endNode)
    {
        List<cell> path = new List<cell>();
        cell currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.m_parent;
        }
        path.Reverse();
        return path;
    }
    public int GetGDist(cell from, cell to)
    {
        int xdiff = (int)(Math.Abs(to.m_x - from.m_x));
        int ydiff = (int)(Math.Abs(to.m_y - from.m_y));
        return xdiff + ydiff;
    }

    //using chebyshev distance heuristic
    public float CalculateHeuristic(cell from, cell to)
    {
        float xdif = (Math.Abs(to.m_x - from.m_x));
        float ydif = (Math.Abs(to.m_y - from.m_y));

        return Math.Max(xdif, ydif);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
