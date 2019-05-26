using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human {

    public int ID, hunger;
    public FiniteState currentState;
    public string stateName;
    public bool hungry;
    public float hungerTick;
    public float nextHungerTick;

    public Human()
    {
        hunger = 100;
        hungerTick = 0.05f;
        currentState = new FiniteState();
        nextHungerTick = Time.time + hungerTick;
        stateName = currentState.ToString();
        hungry = false;
        ID = -1;
    }

    public void updateHunger()
    {
        if (nextHungerTick < Time.time)
        {
            hunger -= 1;
            nextHungerTick = Time.time + hungerTick;
            //Debug.Log("Hunger: " + hunger);
            /*
            int test;

            if (hunger > 50)
            {
                test = Random.Range(0, 400);
                if (test < (100 - hunger) / 16)
                {
                    hungry = true;
                }
            }
            else if (hunger > 25)
            {
                test = Random.Range(0, 200);
                if (test < (100 - hunger) / 6)
                {
                    hungry = true;
                }
            }
            else
            {
                test = Random.Range(0, 100);
                if (test < 100 - hunger)
                {
                    hungry = true;
                }
            }
            */
        }
    }

    public virtual void calculateDesirability() { }

    public void UpdateState(FiniteState state)
    {
        currentState = state;
    }

    public string GetStateString()
    {
        return currentState.ToString();
    }
}
