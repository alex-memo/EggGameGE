using System.Collections.Generic;
using UnityEngine;

public class ChickenPool : InstanceFactory<ChickenPool>
{
	[SerializeField] private Chicken ChickenPrefab;
	[SerializeField] private Chicken EvilChickenPrefab;
	protected List<Chicken> chickens = new();
	protected List<Chicken> evilChickens = new();
	private Vector3 getSpawnPos => WaypointManager.Instance.GetSpawnPoint;
	public void GetChicken()
	{
		if (chickens.Count == 0)
		{//if no chickens in pool, create new one
			spawnNewChicken(ChickenPrefab);
		}
		//we get the first chicken that is disabled from the pool
		getChickenFromPool(chickens, ChickenPrefab, false);

		//random chance to spawn evil chicken
		if (Random.Range(0, 100) < 10)
		{
			GetEvilChicken();
		}
	}
	public void GetEvilChicken()
	{
		if (evilChickens.Count == 0)
		{//if no chickens in pool, create new one
			spawnNewChicken(EvilChickenPrefab, true);
		}
		//we get the first chicken that is disabled from the pool
		Debug.Log("Getting evil chicken");
		getChickenFromPool(evilChickens, EvilChickenPrefab, true);
	}
	public bool AreThereChickenAlive()
	{
		return chickens.Exists(chicken => chicken.gameObject.activeSelf);
	}
	/// <summary>
	/// Returns a random alive chicken
	/// </summary>
	/// <returns></returns>
	public Transform GetAliveChicken()
	{
		List<Chicken> _chickens = chickens.FindAll(chicken => chicken.gameObject.activeSelf);
		if (_chickens.Count == 0) { return null; }
		return _chickens[Random.Range(0, _chickens.Count - 1)].transform;
	}
	private void getChickenFromPool(List<Chicken> _chickens, Chicken _chickenPrefab, bool _isEvil)
	{
		Chicken _chicken = _chickens.Find(chicken => !chicken.gameObject.activeSelf);
		if (_chicken == null)
		{
			spawnNewChicken(_chickenPrefab, _isEvil);
		}
		else
		{
			_chicken.transform.position = getSpawnPos;
			_chicken.gameObject.SetActive(true);
		}
	}
	private void spawnNewChicken(Chicken _chicken, bool _isEvil = false)
	{
		Chicken chicken = Instantiate(_chicken, getSpawnPos, Quaternion.identity, transform);
		if (_isEvil)
		{
			evilChickens.Add(chicken);
			return;
		}
		chickens.Add(chicken);
	}

}
