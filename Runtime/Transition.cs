using UnityEngine;
using System.Collections.Generic;
using System;

namespace Sickbow.StateMachines {
[Serializable]
public class Transition
{
    [SerializeField, HideInInspector] string name;
    public void UpdateName(){
        if (to != null){
            name = "To " + to.name;
        }
    } 
    public Condition GetCondition() => condition;

    [SerializeField] Condition condition;
    [SerializeField] State to;

    public State GetTo() => to;
}
}
