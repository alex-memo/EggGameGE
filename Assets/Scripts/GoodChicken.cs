using UnityEngine;

public class GoodChicken : Chicken
{
	protected override void OnEnable()
	{
		base.OnEnable();
		Navigate();
	}
	protected override void Navigate()
	{
		agent.SetDestination(WaypointManager.Instance.GetRandomWaypoint());
	}
}
