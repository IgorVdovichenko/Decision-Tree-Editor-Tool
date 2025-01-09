using System;
using System.Collections;
using System.Collections.Generic;
using Decision_Tree_Maker;
using UnityEngine;

public class AIExample : MonoBehaviour
{
    private DecisionTreeUser dt;
    void Start()
    {
        dt = GetComponent<DecisionTreeUser>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
            dt.RunTree();
    }
}
