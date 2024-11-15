using System.Collections;
using UnityEngine;

public class GameManager : InstanceFactory<GameManager>
{

	private float lastKeyTime;
	private Coroutine evaluateMultiplierCoroutine;
	private float multiplier = 1;//i wouldve used my dvar for this but thank you gabe for not letting me use it
	public int Score { get; private set; }//i would also use dvar for this 
	public OnMultiplierChange OnMultiplierChange;
	public OnScoreChange OnScoreChange;

	private void Update()
	{
		inputManager();
	}
	public void AddScore()
	{
		++Score;
		OnScoreChange?.Invoke(Score);
	}
	private void inputManager()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("SwitchMode!");
			//ChickenPool.Instance.GetChicken();
			return;
		}
		if (Input.anyKeyDown)
		{
			ChickenPool.Instance.GetChicken();
			bonusChicken();
			lastKeyTime = Time.time;
			evaluateMultiplierCoroutine ??= StartCoroutine(evaluateMultiplier());
		}
	}
	private void bonusChicken()
	{
		//we evaluate the multiplier here
		//and we determine if we should spawn bonus chickens
		int _intMultiplier = (int)multiplier;
		//if multiplier is more than one we spawn that amount of extra chickens
		if (_intMultiplier > 2)
		{
			for (int i = 0; i < _intMultiplier - 1; i++)
			{
				ChickenPool.Instance.GetChicken();
			}
			return;
		}
		//we get the left over decimal multiplier
		float _decimalMultiplier = multiplier - _intMultiplier;
		//we get a random number between 0 and 1
		float _random = Random.Range(0f, 1f);
		//if the random number is less than the decimal multiplier
		//we spawn a bonus chicken
		if (_random < _decimalMultiplier)
		{
			//Debug.LogWarning("Bonus Chicken!");
			ChickenPool.Instance.GetChicken();
		}
	}

	private IEnumerator evaluateMultiplier()
	{
		while (lastKeyTime + 1f > Time.time)
		{
			// Debug.Log("Multiplier!");
			multiplier += 0.1f;
			OnMultiplierChange?.Invoke(multiplier);
			yield return new WaitForSeconds(1);
		}
		multiplier = 1;
		OnMultiplierChange?.Invoke(multiplier);
		evaluateMultiplierCoroutine = null;
	}
}
public delegate void OnMultiplierChange(float _multiplier);
public delegate void OnScoreChange(int _newScore);
