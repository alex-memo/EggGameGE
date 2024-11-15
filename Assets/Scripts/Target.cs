using UnityEngine;

public class Target : MonoBehaviour
{
	private void OnTriggerEnter(Collider _coll)
	{
		//Debug.Log("I got a collision!");
		if (!_coll.gameObject.CompareTag("Chicken")) { return; }
		//Debug.Log("I got a chicken!");
		_coll.gameObject.SetActive(false);
		GameManager.Instance.AddScore();
	}
}
