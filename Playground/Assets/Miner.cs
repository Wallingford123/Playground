using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Human
{
    public int heldGold, bankedGold;
    public MinerWife wife;

    public int[] desirabilities;

    public Miner()
    {
        desirabilities = new int[3];
        heldGold = 0;
        bankedGold = 0;
        UpdateState(new Mining());
    }

    public override void calculateDesirability()
    {
        int k = 1;

        desirabilities[0] = 65; //Mining
        //Debug.Log("Mining: " + desirabilities[0]);
        desirabilities[1] = k*(Mathf.FloorToInt((100 - hunger) / (((float)hunger + 50) / 90))); //Eating
        //Debug.Log("Eating: " + desirabilities[1]);
        desirabilities[2] = (Mathf.FloorToInt((heldGold*5) * ((float)(100-desirabilities[1])/50))); //Banking
        //Debug.Log("Banking: " + desirabilities[2]);

        int t = 0;
        int i = 0;
        int p = 0;
        foreach (int d in desirabilities)
        {
            if (d > t) {
                t = d;
                p = i;
            }
            i++;
        }

        switch (p) {
            case 0:
                if (GetStateString() != "Mining")
                    UpdateState(new Mining());
                break;
            case 1:
                if(GetStateString() != "Eating")
                    UpdateState(new Eating());
                break;
            case 2:
                if (GetStateString() != "Banking")
                    UpdateState(new Banking());
                break;
        }

    }


}

