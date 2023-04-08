using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Sickbow.StateMachines {
[Serializable]
public class Condition 
{
    [SerializeField] string name;
    [SerializeField] bool isSatisfied;
    [SerializeField] List<ConditionValue> conditionValues;
    
    public bool GetValue(){
        if (conditionValues.Count == 0) {
            isSatisfied = false;
            return false;
        }

        foreach(ConditionValue cv in conditionValues){
            if (cv.value == false){
                isSatisfied = false;
                return false;
            }
        }
        isSatisfied = true;
        return true;
    }
}
}