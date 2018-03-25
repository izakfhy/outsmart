using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPathState : State {

    public FindPathState(creep cp) : base(cp) { }
	// Use this for initialization
	override public void OnStateEnter () {
        var next = m_creep.m_path[0];
        Vector3 p = m_creep.m_gridarr[(int)next.m_x, (int)next.m_y].m_pos;

        m_creep.m_target = p;

        m_creep.m_path.Remove(next);
        //m_state = "moving";
        m_creep.SetState(new MovingState(m_creep));
	}

    override public void OnStateExit()
    {

    }
    override public void OnStateUpdate()
    {

    }
	
}
