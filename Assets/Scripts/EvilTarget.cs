using UnityEngine;

public class EvilTarget : MonoBehaviour
{
	private void OnTriggerEnter(Collider _coll)
	{
		if (!_coll.gameObject.CompareTag("Evil")) { return; }
		_coll.gameObject.SetActive(false);
		Debug.Log("Evil target hit!");
	}
}
