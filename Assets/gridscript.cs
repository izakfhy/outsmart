using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cell 
{
    public Vector3 m_pos;
    public Vector3 m_size;
    public int m_val;
    
}
public class gridscript : MonoBehaviour {

  

    const int m_gridnum = 10;//10x 10 grid.
    public GameObject m_floor;
    cell[,] m_gridarr = new cell[m_gridnum, m_gridnum];
    Vector2 m_sizes;
    float m_cellsize = 0.0f;

    public GameObject m_test;
    public GameObject[] m_wallArr;
	// Use this for initialization
	void Start () {
        
        for (int i = 0; i < m_gridarr.GetLength(0); i++)
        {
            for (int j = 0; j < m_gridarr.GetLength(1); j++)
            {
                m_gridarr[i,j] = new cell();
            }
        }

        m_floor = GameObject.Find("floor");
        m_test = GameObject.Find("wall");
        MeshFilter m = m_floor.GetComponent<MeshFilter>();
        
        m_sizes = m.mesh.bounds.size;
        m_cellsize = (int)m_sizes.x / m_gridnum;

        int bound0 = m_gridarr.GetUpperBound(0);
        int bound1 = m_gridarr.GetUpperBound(1);

        for (int i = 0; i < m_gridarr.GetLength(0); i++)
        {
            for (int j = 0; j < m_gridarr.GetLength(1); j++)
            {
                var obj = m_gridarr[i, j];
                m_gridarr[i, j].m_size.x = m_cellsize;
                m_gridarr[i, j].m_size.z = m_cellsize;

                m_gridarr[i, j].m_pos.x = i * m_cellsize + (m_cellsize / 2);
                m_gridarr[i, j].m_pos.z = j * m_cellsize + (m_cellsize / 2);
                m_gridarr[i, j].m_pos.y = 0.5f;
            }
            
        }
        var testnew = Instantiate(m_test, m_gridarr[0, 2].m_pos, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
