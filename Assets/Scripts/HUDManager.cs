using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
	[SerializeField] private TMP_Text multiplierText;
	[SerializeField] private TMP_Text scoreText;
	private void Start()
	{
		GameManager.Instance.OnScoreChange += OnScoreChange;
		GameManager.Instance.OnMultiplierChange += OnMultiplierChange;
	}
	private void OnMultiplierChange(float multiplier)
	{
		multiplierText.text = $"Multiplier: {multiplier}";
	}
	private void OnScoreChange(int score)
	{
		scoreText.text = $"Score: {score}";
	}
}
