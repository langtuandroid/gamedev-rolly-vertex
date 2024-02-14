using UnityEngine;

public class LevelPassrv : MonoBehaviour
{
	public static LevelPassrv Instancerv;
	public GameObject levelPassPrefab;
	
	private GameObject levelPass;
	private LevelPassDestruction levelPassDestruction;
	
	private void Awake()
	{
		Instancerv = this;
	}
	
	private void Start()
	{
		levelPass = Instantiate(levelPassPrefab);
		
		levelPass.transform.position = Vector3.forward * 12.5f;

		levelPassDestruction = levelPass.GetComponent<LevelPassDestruction>();
		
		PlaceNewLevelPass();
	}
	
	public void PlaceNewLevelPass()
	{
		Vector3 newPos = levelPass.transform.position;
		newPos.z += GameManager.Instance.levelMilestone * PlatformPooler.Instance.platformGapDistance;
		levelPass.transform.position = newPos;

		var color = GameManager.Instance.newColor;
		color.a = 0.5f;

		levelPass.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;

		var renderers = levelPass.GetComponentsInChildren<MeshRenderer>();

		for (int i = 0; i < renderers.Length; i++) {
			renderers[i].material.color = color;
		}

        levelPassDestruction.RestoreLevelPass();
	}

	public void DestroyLevelPass()
	{
		levelPassDestruction.DestroyLevelPass();
	}
}
