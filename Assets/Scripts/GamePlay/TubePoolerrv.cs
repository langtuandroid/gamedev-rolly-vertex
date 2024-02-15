using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubePoolerrv : MonoBehaviour 
{
	public static TubePoolerrv Instancerv;

	[SerializeField]
	private float tubeCount;
	[SerializeField]
	private float tubeGapDistance;
	[SerializeField]
	private GameObject tubePrefab;
	[SerializeField]
	private Transform tubeParent;
	[SerializeField]
	private Vector3 lastTubePosition;
	[SerializeField]
	private List<GameObject> activeTubes;
	[SerializeField]
	private List<GameObject> inactiveTubes;

	private void Awake()
	{
		Instancerv = this;
	}
	
	private void Start () 
	{
		CreateInitialTubesrv();
		PlaceInitialTubesrv();
	}

	private void CreateInitialTubesrv()
	{
		for (int i = 0; i < 30; i++)
		{
			inactiveTubes.Add(
				Instantiate(tubePrefab,tubeParent));
			inactiveTubes[i].SetActive(false);
		}
	}

	private void PlaceInitialTubesrv()
	{
		Vector3 pos = Vector3.zero;
		for (int i = 0; i < tubeCount; i++)
		{
			pos.z = (i * tubeGapDistance) - 20;
			
			CreateTuberv(pos);
		}
	}
	
	private void CreateTuberv( Vector3 _position)
	{
		GameObject tubeToBeCreated = inactiveTubes[0];
		tubeToBeCreated.SetActive(true);
		tubeToBeCreated.transform.position = _position;
		lastTubePosition = _position;
		inactiveTubes.RemoveAt(0);
		activeTubes.Add(tubeToBeCreated);
	}
	
	public void CreateNextPlatformrv()
	{
		Vector3 position = lastTubePosition;
		position.z += tubeGapDistance;
		CreateTuberv(position);
	}
	
	public void DestroyTuberv(GameObject platform, float time)
	{
		StartCoroutine(DestroyTubeCorrv(platform, time));
	}
	
	private void DestroyTuberv(GameObject platform)
	{
		StartCoroutine(DestroyTubeCorrv(platform, 0));
	}

	IEnumerator DestroyTubeCorrv(GameObject platform, float time)
	{
		yield return new WaitForSeconds(time);

		int index = 0;
		for (int i = 0; i < activeTubes.Count; i++)
		{
			if (activeTubes[i] == platform)
			{
				index = i;
				break;
			}
		}
		
		GameObject platformToBeDisabled = activeTubes[index];
		platformToBeDisabled.SetActive(false);
		activeTubes.RemoveAt(index);
		inactiveTubes.Add(platformToBeDisabled);
	}
}
