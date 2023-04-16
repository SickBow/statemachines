using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sickbow.StateMachines {
[Serializable]
public abstract class State : ScriptableObject
{
    [SerializeField] List<Transition> transitions;
    public List<Transition> GetTransitions() => transitions;

    void OnValidate(){
        foreach(Transition tran in transitions){
            tran.UpdateName();
        }
    }

    public virtual void Enter(GameObject owner){}
    public virtual void Exit(GameObject owner){}
    public abstract void Run(GameObject owner);
    
}
}
