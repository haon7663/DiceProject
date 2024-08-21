using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
	public virtual State CurrentState
	{
		get => currentState;
		set => Transition(value);
	}

	[SerializeField] protected State currentState;
	protected bool inTransition;

	public virtual T GetState<T>() where T : State
	{
		T target = GetComponent<T>();
		if (target == null)
		{
			target = gameObject.AddComponent<T>();
		}
		return target;
	}

	public virtual void ChangeState<T>() where T : State
	{
		CurrentState = GetState<T>();
	}

	protected virtual void Transition(State value)
	{
		if (currentState == value || inTransition)
		{
			return;
		}

		inTransition = true;

		if (currentState != null)
		{
			currentState.Exit();
		}

		currentState = value;

		if (currentState != null)
		{
			currentState.Enter();
		}

		inTransition = false;
	}
}
