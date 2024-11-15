using System.Collections;
using UnityEngine;

public class EvilChicken : Chicken
{
	private Transform target;
	private void OnTriggerEnter(Collider _coll)
	{
		if (!_coll.gameObject.CompareTag("Chicken")) { return; }
		_coll.gameObject.SetActive(false);
	}
	protected override void OnEnable()
	{
		base.OnEnable();
		StartCoroutine(trackChicken());
	}
	protected override void Navigate()
	{
		if (target == null) { return; }
		if (!target.gameObject.activeSelf) { return; }
		agent.SetDestination(target.position);
	}
	private IEnumerator trackChicken()
	{
		target = getTarget();
		if (target == null)
		{
			//no chickens alive
			agent.SetDestination(WaypointManager.Instance.GetEvilChickenExit);
			yield break;
		}
		agent.SetDestination(target.position);
		Debug.Log("Tracking chicken");
		while (true)
		{
			Debug.Log("Tracking while chicken");
			if (target == null)
			{//if no target we get a new one
				target = getTarget();
			}
			if (target.gameObject.activeSelf)
			{
				agent.SetDestination(target.position);
				yield return new WaitForSeconds(0.5f);
				continue;
			}//if target is active, we continue
			 //before we get a new target we check if there are chickens alive
			if (!ChickenPool.Instance.AreThereChickenAlive()) { break; }
			//if there are then we get a new target
			target = getTarget();
			yield return new WaitForSeconds(0.5f);//every .5 we check 
		}
		Debug.Log("No chickens alive, making my evil exit!");
		agent.ResetPath();
		yield return new WaitForSeconds(.5f);
		Debug.Log("Evil chicken going out of the screen");
		agent.SetDestination(WaypointManager.Instance.GetEvilChickenExit);
		Debug.Log(agent.destination);
		//if no chickens are alive, we disable the evil chicken by going out of the screen
	}
	private Transform getTarget()
	{
		Debug.Log("Evil chicken getting target");
		return ChickenPool.Instance.GetAliveChicken();
	}
}
