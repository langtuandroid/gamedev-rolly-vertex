using UnityEngine;

public class TubePosition : MonoBehaviour
{
	private GameObject playerHolder;
	private bool destroyed;
	private void Start()
	{
		playerHolder = GameManager.Instance.GetPlayerHolder();
	}
	
	private void Update () 
	{
		if (destroyed)
		{
			return;
		}
		
		if (transform.position.z < playerHolder.transform.position.z + 20f)
		{
			TubePooler.Instance.DestroyTube(gameObject,2f);
			TubePooler.Instance.CreateNextPlatform();
			destroyed = true;
		}
	}

	private void OnEnable()
	{
		destroyed = false;
	}
}
