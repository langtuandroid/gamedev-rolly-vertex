using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay
{
	public class TubePoolerrv : MonoBehaviour 
	{
		public static TubePoolerrv Instancerv;

		[SerializeField]
		private float _tubeCountrv;
		[SerializeField]
		private float _tubeGapDistancerv;
		[SerializeField]
		private GameObject _tubePrefabrv;
		[SerializeField]
		private Transform _tubeParentrv;
		[SerializeField]
		private Vector3 _lastTubePositionrv;
		[SerializeField]
		private List<GameObject> _activeTubesrv;
		[SerializeField]
		private List<GameObject> _inactiveTubesrv;

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
				_inactiveTubesrv.Add(
					Instantiate(_tubePrefabrv,_tubeParentrv));
				_inactiveTubesrv[i].SetActive(false);
			}
		}

		private void PlaceInitialTubesrv()
		{
			Vector3 pos = Vector3.zero;
			for (int i = 0; i < _tubeCountrv; i++)
			{
				pos.z = (i * _tubeGapDistancerv) - 20;
			
				CreateTuberv(pos);
			}
		}
	
		private void CreateTuberv( Vector3 _position)
		{
			GameObject tubeToBeCreated = _inactiveTubesrv[0];
			tubeToBeCreated.SetActive(true);
			tubeToBeCreated.transform.position = _position;
			_lastTubePositionrv = _position;
			_inactiveTubesrv.RemoveAt(0);
			_activeTubesrv.Add(tubeToBeCreated);
		}
	
		public void CreateNextPlatformrv()
		{
			Vector3 position = _lastTubePositionrv;
			position.z += _tubeGapDistancerv;
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
			for (int i = 0; i < _activeTubesrv.Count; i++)
			{
				if (_activeTubesrv[i] == platform)
				{
					index = i;
					break;
				}
			}
		
			GameObject platformToBeDisabled = _activeTubesrv[index];
			platformToBeDisabled.SetActive(false);
			_activeTubesrv.RemoveAt(index);
			_inactiveTubesrv.Add(platformToBeDisabled);
		}
		
		private bool IsPrimtrv(int number)
		{
			if (number <= 1) return false;
			if (number <= 3) return true;
			if (number % 2 == 0 || number % 3 == 0) return false;
			for (int i = 5; i * i <= number; i += 6)
			{
				if (number % i == 0 || number % (i + 2) == 0) return false;
			}
			return true;
		}
	}
}
