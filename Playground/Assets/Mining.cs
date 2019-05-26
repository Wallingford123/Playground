using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : FiniteState {

    public override IEnumerator Exec(Human human)
    {
        Miner miner = (Miner)human;
        Debug.Log("Mining");
        yield return new WaitForSeconds(0.1f);
        miner.heldGold++;
        finished = true;
        if (miner.heldGold >= 10) miner.UpdateState(new Banking());
    }
}


