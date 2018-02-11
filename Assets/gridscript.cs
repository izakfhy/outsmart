using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cell : IComparable 
{
    public Vector3 m_pos;
    public Vector3 m_size;
    public int m_val;
    public float m_G = 0.0f, m_H = 0.0f;
    public float m_addCost;
    public int m_x = 0, m_y = 0;
    public cell m_parent;

    public static bool operator ==(cell lhs, cell rhs)
    {
        if (!object.ReferenceEquals(lhs, null) && !object.ReferenceEquals(rhs, null))
            return rhs.m_x == lhs.m_x && rhs.m_y == lhs.m_y;
        else
            return object.ReferenceEquals(rhs, null) && object.ReferenceEquals(lhs, null);
    }
    public static bool operator !=(cell lhs, cell rhs)
    {
        if (!object.ReferenceEquals(lhs, null) && !object.ReferenceEquals(rhs, null))
            return rhs.m_x != lhs.m_x || rhs.m_y != lhs.m_y;
        else
            return object.ReferenceEquals(lhs, null) && object.ReferenceEquals(rhs, null);
    }
    public static bool operator< (cell lhs, cell rhs)
    {
        if (!object.ReferenceEquals(lhs, null) && !object.ReferenceEquals(rhs, null))
        {
            var f1 = lhs.m_G + lhs.m_H;
            var f2 = rhs.m_G + rhs.m_H;
            return f1 < f2;
        }
        else
            return false;
       
    }
    public static bool operator >(cell lhs, cell rhs)
    {
        if (!object.ReferenceEquals(lhs, null) && !object.ReferenceEquals(rhs, null))
        {
            var f1 = lhs.m_G + lhs.m_H;
            var f2 = rhs.m_G + rhs.m_H;
            return f1 > f2;
        }
        else
            return false;
        
    }
    public  int CompareTo(object rhs) 
    {
        cell r = (cell)rhs;
        var f1 = this.m_G + this.m_H;
        var f2 = r.m_G + r.m_H;

        if (f1 < f2)
            return -1;
        else if (f1 > f2)
            return 1;
        else
            return 0;
    }
}

public enum cellVal
{
    empty=0,
    wall=1,
    creep=2
}
public class gridscript : MonoBehaviour {

    //public string[] m_cellvalsmap = new string[] { "", "floor", "creep" };

    const int m_gridnum = 10;//10x 10 grid.
    public GameObject m_floor;
    cell[,] m_gridarr = new cell[m_gridnum, m_gridnum];
    Vector2 m_sizes;
    float m_cellsize = 0.0f;

    public GameObject m_test;
    public GameObject[] m_wallArr;
    public GameObject m_creep;
    public pathfinding m_pathfinder;
    public List<cell> m_path;

    //test variables, this is to be in a seperate class for creep
    public float m_speed = 1.0f;

	// Use this for initialization
	void Start () {
        
        for (int i = 0; i < m_gridarr.GetLength(0); i++)
        {
            for (int j = 0; j < m_gridarr.GetLength(1); j++)
            {
                m_gridarr[i,j] = new cell();
                m_gridarr[i, j].m_x = i;
                m_gridarr[i, j].m_y = j;
            }
        }
        m_pathfinder = new pathfinding(m_gridarr);
        
        m_floor = GameObject.Find("floor");
        m_test = GameObject.Find("wall");
        m_creep = GameObject.Find("creep");

        MeshFilter m = m_floor.GetComponent<MeshFilter>();
        
        m_sizes = m.mesh.bounds.size;
        m_cellsize = (int)m_sizes.x / m_gridnum;

        int bound0 = m_gridarr.GetUpperBound(0);
        int bound1 = m_gridarr.GetUpperBound(1);

        //test spawn vals
        m_gridarr[0, 2].m_val = (int)cellVal.wall;
        m_gridarr[4, 1].m_val = (int)cellVal.wall;
        m_gridarr[5, 1].m_val = (int)cellVal.wall;
        m_gridarr[6, 1].m_val = (int)cellVal.wall;
        m_gridarr[3, 1].m_val = (int)cellVal.wall;
        m_gridarr[7, 1].m_val = (int)cellVal.wall;
        m_gridarr[8, 1].m_val = (int)cellVal.wall;
        m_gridarr[5, 0].m_val = (int)cellVal.creep;

        cell target = m_gridarr[5, 9];

        for (int i = 0; i < m_gridarr.GetLength(0); i++)
        {
            for (int j = 0; j < m_gridarr.GetLength(1); j++)
            {
                //initialize position and size
                m_gridarr[i, j].m_size.x = m_cellsize;
                m_gridarr[i, j].m_size.z = m_cellsize;

                m_gridarr[i, j].m_pos.x = i * m_cellsize + (m_cellsize / 2);
                m_gridarr[i, j].m_pos.z = j * m_cellsize + (m_cellsize / 2);
                m_gridarr[i, j].m_pos.y = 0.5f;

                //initialize cell value and instantiate objects
                InitGridVal(i, j);
            }
            
        }
        /*m_path = m_pathfinder.FindPath(m_gridarr[5, 0], target);
        foreach(var cell in m_path)
        {
            Instantiate(GameObject.Find("creep"), m_gridarr[cell.m_x, cell.m_y].m_pos, Quaternion.identity);
        }*/
        //var testnew = Instantiate(m_test, m_gridarr[0, 2].m_pos, Quaternion.identity);
        //var spawncreep = Instantiate(m_creep, m_gridarr[m_gridnum/2, 0].m_pos, Quaternion.identity);
	}
	
    void InitGridVal(int x, int y)
    {
        cellVal elem = (cellVal)Enum.Parse(typeof(cellVal), m_gridarr[x,y].m_val.ToString());
        Console.WriteLine(elem.ToString());
        var temp = elem.ToString();
        if(temp != "empty")
        {
            var newobj = Instantiate(GameObject.Find(elem.ToString()), m_gridarr[x, y].m_pos, Quaternion.identity);
            if (temp == "creep")
            {
                creep newcreep = newobj.GetComponent<creep>();
                newcreep.InitGrid(m_gridarr);
            }
               
        }
          
    }
	// Update is called once per frame
	void Update () {
       
	}
}
