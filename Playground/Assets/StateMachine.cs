using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    public int numberOfMiners;

    private Miner[] miners;
    private MinerWife[] wives;

	void Start () {
        miners = new Miner[numberOfMiners];
        wives = new MinerWife[numberOfMiners];
        for(int i = 0; i < numberOfMiners; i++)
        {
            miners[i] = new Miner();
            miners[i].ID = i;
            wives[i] = new MinerWife();
            wives[i].ID = i;
            miners[i].wife = wives[i];
            wives[i].husband = miners[i];
        }
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAllPeople();
	}

    void UpdateAllPeople()
    {
        foreach (Miner m in miners)
        {
            if (m.currentState.finished)
            {
                /*
                if (m.hungry && m.wife.foodMade)
                {
                    Eating eState = new Eating();
                    eState.previousState = m.currentState;
                    m.currentState = eState;
                    m.hungry = false;
                    m.wife.foodMade = false;
                }*/
                m.calculateDesirability();
                StartCoroutine(m.currentState.Exec(m));
                m.currentState.finished = false;
            }
            if (m.GetStateString() != "Eating" && !m.hungry && m.GetStateString() != "Banking") 
                m.updateHunger();
        }

        /*foreach (MinerWife m in wives)
        {
            if (m.currentState.finished)
            {
                if (m.hungry) m.UpdateState(new Cooking());
                StartCoroutine(m.currentState.Exec(m));
                m.currentState.finished = false;
                m.hungry = false;
            }
            if (m.GetStateString() != "Eating" && !m.hungry && m.GetStateString() != "Cooking")
                m.updateHunger();
        }*/

    }
}
