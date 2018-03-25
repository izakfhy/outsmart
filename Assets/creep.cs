using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class creep : MonoBehaviour {

    public float m_speed = 1.0f;
    public List<cell> m_path;
    public cell[,] m_gridarr;
    public pathfinding m_pathfinder;
    public Vector2 m_initcellPos;
    public State m_currentState;


    //this is hackish "state machine" variable
    //i will write a proper state machine next time
    public string m_state = "idle";
    public Vector3 m_target;
    public creep()
    {
        m_initcellPos.x = 5;
        m_initcellPos.y = 0;
        m_path = new List<cell>();
    
    }

    public void InitGrid(cell[,] grid)
    {
         m_gridarr = grid;
         m_pathfinder = new pathfinding(m_gridarr);
         cell target = m_gridarr[5, 9];
         PathFind(target);
        
    }
	// Use this for initialization
	void Start () {
        m_currentState = new Idle(this);
	}

    public void SetState(State state)
    {
        if (m_currentState != null)
            m_currentState.OnStateExit();
        m_currentState = state;
        if (m_currentState != null)
            m_currentState.OnStateEnter();
    }
    void PathFind(cell target)
    {
        m_path = m_pathfinder.FindPath(m_gridarr[(int)m_initcellPos.x, (int)m_initcellPos.y], target);
    }
	
	// Update is called once per frame
	void Update () {
        if(m_currentState != null)
        m_currentState.OnStateUpdate();
        /*if (m_state == "idle")
        {
            if (m_path.Count > 0)
                m_state = "settarget";
        }
        else if(m_state == "settarget")
        {
            var next = m_path[0];
            Vector3 p = m_gridarr[(int)next.m_x, (int)next.m_y].m_pos;

            m_target = p;
        
            m_path.Remove(next);
            m_state = "moving";
        }
        else if(m_state == "moving")
        {

            float step = m_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
            Vector3 curpos = transform.position;
            Vector3 vec = m_target - curpos;

            if(Math.Abs(vec.magnitude) < 0.000001f)
            {
                m_state = "idle";
            }
        }*/
        
        
	}
}
