using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerWife : Human
{
    public bool foodMade;
    public Miner husband;


    public MinerWife()
    {
        foodMade = false;
        UpdateState(new Chilling());
    }
    /*
    public void cook()
    {
        if (nextHungerTick < Time.time)
        {
            hunger -= 1;
            nextHungerTick = Time.time + hungerTick;
            //Debug.Log("Hunger: " + hunger);

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

        }
    }*/
}
