using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {

    /// <summary>
    /// Called when the State is Entered.
    /// </summary>
    public virtual void Enter () {
        AddListeners();
    }

    ////// <summary>
    /// Called when the State is Exited.
    /// </summary>
    public virtual void Exit () {
        RemoveListeners();
    }

    protected virtual void OnDestroy () {
        RemoveListeners();
    }

    protected virtual void AddListeners () {

    }

    protected virtual void RemoveListeners () {

    }
}