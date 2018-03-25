using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingState : State {
    float m_step = 0.0f;
    public MovingState(creep cp) : base(cp) { }
    override public  void OnStateEnter()
    {

        m_step = m_creep.m_speed * Time.deltaTime;
       
    }

    override public  void OnStateExit()
    {

    }
    override public  void OnStateUpdate()
    {
        m_creep.transform.position = Vector3.MoveTowards(m_creep.transform.position, m_creep.m_target, m_step);
        Vector3 curpos = m_creep.transform.position;
        Vector3 vec = m_creep.m_target - curpos;

        if (Math.Abs(vec.magnitude) < 0.000001f)
        {
            m_creep.SetState(new Idle(m_creep));
        }
    }
}
