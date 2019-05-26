using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chilling : FiniteState {

    public override IEnumerator Exec(Human human)
    {
        MinerWife wife = (MinerWife)human;
        //Debug.Log("Chilling");
        yield return new WaitForSeconds(0.1f);
        finished = true;
    }
}
