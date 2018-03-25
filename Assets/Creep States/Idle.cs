using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State {

    public Idle(creep cp) : base(cp) { }
	// Use this for initialization
    override public void OnStateEnter()
    {
        if(m_creep.m_path.Count > 0)
        {
            m_creep.SetState(new FindPathState(m_creep));
        }
    }

    override public void OnStateExit()
    {

    }
    override public void OnStateUpdate()
    {
        if (m_creep.m_path.Count > 0)
        {
            m_creep.SetState(new FindPathState(m_creep));
        }
    }
}
