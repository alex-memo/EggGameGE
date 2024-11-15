using UnityEngine;
using UnityEngine.AI;

public abstract class Chicken : MonoBehaviour
{
	protected NavMeshAgent agent;
	protected virtual void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}
	protected virtual void OnEnable()
	{
		//Debug.Log("I spawned!");
	}
	protected abstract void Navigate();
	
}
