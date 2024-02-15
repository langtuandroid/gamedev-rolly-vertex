using UnityEngine;

namespace GamePlay
{
	public class LevelPassrv : MonoBehaviour
	{
		public static LevelPassrv Instancerv;
		
		[SerializeField]
		private GameObject _levelPassPrefabrv;
		private GameObject _levelPassrv;
		private LevelPassDestructionrv _levelPassDestructionrv;
	
		private void Awake()
		{
			Instancerv = this;
		}
	
		private void Start()
		{
			_levelPassrv = Instantiate(_levelPassPrefabrv);
		
			_levelPassrv.transform.position = Vector3.forward * 12.5f;

			_levelPassDestructionrv = _levelPassrv.GetComponent<LevelPassDestructionrv>();
		
			PlaceNewLevelPassrv();
		}
	
		public void PlaceNewLevelPassrv()
		{
			Vector3 newPos = _levelPassrv.transform.position;
			newPos.z += GameManager.Instance.LevelMilestonerv * PlatformPoolerrv.Instancerv.PlatformGapDistancerv;
			_levelPassrv.transform.position = newPos;

			var color = GameManager.Instance.newColor;
			color.a = 0.5f;

			_levelPassrv.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;

			var renderers = _levelPassrv.GetComponentsInChildren<MeshRenderer>();

			for (int i = 0; i < renderers.Length; i++) {
				renderers[i].material.color = color;
			}

			_levelPassDestructionrv.RestoreLevelPass();
		}

		public void DestroyLevelPassrv()
		{
			_levelPassDestructionrv.DestroyLevelPass();
		}
	}
}
