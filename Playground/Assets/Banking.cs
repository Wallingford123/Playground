using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banking : FiniteState {

    int currentStage = 0;
    bool done = false;

    public override IEnumerator Exec(Human human)
    {
        Miner miner = (Miner)human;
        switch (currentStage) {
            case 0:
                Debug.Log("Going to Bank");
                break;
            case 1:
                Debug.Log("Banked Gold");
                done = true;
                break;
        }
        yield return new WaitForSeconds(0.1f);
        finished = true;
        if (done)
        {
            miner.bankedGold += miner.heldGold;
            miner.heldGold = 0;
        }
        currentStage++;
    }
}
