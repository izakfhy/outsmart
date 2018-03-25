using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {

    protected creep m_creep;
    public State(creep cp)
    {
        m_creep = cp;
    }
	// Use this for initialization
	public virtual void OnStateEnter () {
		
	}

    public virtual void OnStateExit()
    {

    }
    public abstract void OnStateUpdate();


}
