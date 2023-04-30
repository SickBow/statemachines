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
    [SerializeField] bool invert;
    [SerializeField] bool isSatisfied;
    [SerializeField] List<ConditionValue> conditionValues;
    
    public string GetName() => name;
    public bool GetInvert() => invert;
    public List<ConditionValue> GetConditionValues() => conditionValues;

    public bool GetValue(){
        if (conditionValues.Count == 0) {
            isSatisfied = false;
            return false;
        }

        foreach(ConditionValue cv in conditionValues){
            if (cv.value == false){
                isSatisfied = invert? true : false;
                return invert? true : false;
            }
        }
        isSatisfied = invert? false : true;
        return invert? false : true;
    }

    public void Init(string name, bool invert, List<ConditionValue> conditionValues){
        this.name = name;
        this.invert = invert;
        this.conditionValues = conditionValues;
    }
}
}
