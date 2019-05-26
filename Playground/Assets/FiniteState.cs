using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteState{

    public bool finished = true;
    public virtual IEnumerator Exec(Human human)
    {
        //Debug.Log("wat");
        yield return new WaitForSeconds(0.1f);
        finished = true;
    }
}
