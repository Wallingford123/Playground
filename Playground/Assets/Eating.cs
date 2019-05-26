using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : FiniteState {

    int currentStage = 0;
    bool done = false;

    public override IEnumerator Exec(Human human)
    {
        switch (currentStage)
        {
            case 0:
                Debug.Log(human.ToString() + ": Eating");
                break;
            case 1:
                Debug.Log(human.ToString() + ": Eating");
                done = true;
                break;
        }

        yield return new WaitForSeconds(0.1f);
        finished = true;
        if (done)
        {
            human.hunger = 100;
        }
        currentStage++;
    }
}
