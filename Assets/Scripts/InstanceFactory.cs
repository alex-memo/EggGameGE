using UnityEngine;

public class InstanceFactory<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance;
	protected virtual void Awake()
	{
		if(Instance!= null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this as T;
	}
}
