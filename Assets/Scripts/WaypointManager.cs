using UnityEngine;

public class WaypointManager : InstanceFactory<WaypointManager>
{
	[SerializeField] private Transform spawnPoint;
	public Vector3 GetSpawnPoint => spawnPoint.position;
	[SerializeField] private Transform[] waypoints;
	[SerializeField] private Transform evilChickenWaypoint;
	public Vector3 GetEvilChickenExit => evilChickenWaypoint.position;
	public Vector3 GetRandomWaypoint()
	{
		return waypoints[Random.Range(0, waypoints.Length)].position;
	}
}
