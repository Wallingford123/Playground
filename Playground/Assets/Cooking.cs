using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : FiniteState {

    int currentStage = 0;
    bool done = false;

    public override IEnumerator Exec(Human human)
    {
        MinerWife wife = (MinerWife)human;
        switch (currentStage)
        {
            case 0:
                //Debug.Log(wife.ToString() + ": Cooking");
                break;
            case 1:
                //Debug.Log(wife.ToString() + ": Cooking");
                break;
            case 2:
                //Debug.Log(wife.ToString() + ": Cooking");
                done = true;
                break;
        }
        yield return new WaitForSeconds(0.1f);
        finished = true;
        if (done)
        {
            wife.foodMade = true;
            wife.hunger = 100;
            wife.UpdateState(new Chilling());
        }
        currentStage++;
    }
}
