using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
	public class PlatformPoolerrv : MonoBehaviour
	{
		public static PlatformPoolerrv Instancerv;
        [SerializeField]
		private float _initialPlatformCountrv;
		[SerializeField]
		private float _platformGapDistancerv;
		[SerializeField]
		private Vector3 _lastPlatformPositionrv;
		[SerializeField]
		private Vector3 _lastPlatformRotationrv;
		[SerializeField]
		private List<GameObject> _activePlatformsrv;
		[SerializeField]
		private List<GameObject> _inactivePlatformsrv;
		[SerializeField]
		private GameObject _platformPrefabrv;
		[SerializeField]
		private Transform _platformParentrv;

		public float PlatformGapDistancerv => _platformGapDistancerv;

		public List<GameObject> ActivePlatformsrv => _activePlatformsrv;
		

		private void Awake()
		{
			Instancerv = this;
		}
	
		private void Start () 
		{
			CreateInitialPlatformsrv();
			PlaceInitialPlatformsrv();
		}

		private void CreateInitialPlatformsrv()
		{
			for (int i = 0; i < 30; i++)
			{
				_inactivePlatformsrv.Add(
					Instantiate(_platformPrefabrv,_platformParentrv));
				_inactivePlatformsrv[i].SetActive(false);
			}
		}

		private void PlaceInitialPlatformsrv()
		{
			Vector3 pos = Vector3.zero;
			Vector3 rot = Vector3.zero;
			for (int i = 0; i < 4; i++)
			{
				pos.z = i * _platformGapDistancerv;
			
				CreatePlatformrv(pos,rot, true);
			}

			for (int i = 0; i < _initialPlatformCountrv; i ++)
			{
				CreateNextPlatformrv();
			}
		}

		public void DestroyPlatformrv(GameObject platform, float time)
		{
			StartCoroutine(DestroyPlatformCorrv(platform, time));
		}
	
		private void DestroyPlatformrv(GameObject platform)
		{
			StartCoroutine(DestroyPlatformCorrv(platform, 0));
		}

		private IEnumerator DestroyPlatformCorrv(GameObject platform, float time)
		{
			// Wait the wanted amount to destroy te platform
			yield return new WaitForSeconds(time);

			// Finds the platform to be destoryed
			int index = 0;
			for (int i = 0; i < _activePlatformsrv.Count; i++)
			{
				if (_activePlatformsrv[i] == platform)
				{
					index = i;
					break;
				}
			}
		
			// Gets the platform to be destroyed
			GameObject platformToBeDisabled = _activePlatformsrv[index];
		
			// Disables the platform
			platformToBeDisabled.SetActive(false);
		
			// Gets the platfrom component for future uses
			Platformrv platformrv = platformToBeDisabled.GetComponent<Platformrv>();
		
			// Resets the platform values
			platformrv.ResetEverythingrv();
		
			// Removes the platform from the active list and add to the inactive list
			_activePlatformsrv.RemoveAt(index);
			_inactivePlatformsrv.Add(platformToBeDisabled);
		}

		private void CreatePlatformrv( Vector3 _position, Vector3 _rotation, bool dullPlatform )
		{
			// Gets the first platform from the inactive list
			GameObject platformToBeCreated = _inactivePlatformsrv[0];
		
			// Activates the platform applies the rotation anf the position
			platformToBeCreated.SetActive(true);
			platformToBeCreated.transform.position = _position;
			platformToBeCreated.transform.rotation = Quaternion.Euler(_rotation);

			// Gets the platform component for future applications
			Platformrv platformrv = platformToBeCreated.GetComponent<Platformrv>();
		
			// Picks a random value between 0 - 100
			float randValue = Random.Range(0f, 100f);

			// if it is a dull platform dont change anything
			if (!dullPlatform)
			{
				// There is a 12.5 percent chance that the platform will have a perfect
				if (randValue <= 12.5f)
				{
					platformrv.SetPerfectrv();
				}
		
				// There is a 5 percent chance that the platform will be able to move
				if (randValue <= PlayerStatsrv.LevelHardnessMultiplierrv)
				{
					platformrv.SetMoveablerv();
				}
		
				// There is a 10 percent chance that the platform will be at a random size
				if (randValue <= 50f + PlayerStatsrv.LevelHardnessMultiplierrv)
				{
					platformrv.RandomSizerv();
				}
			}
		
			// Saves the platforms rotation and the position for future uses
			_lastPlatformPositionrv = _position;
			_lastPlatformRotationrv = _rotation;
		
			// Removes the platform from the inactive list and add to the active list
			_inactivePlatformsrv.RemoveAt(0);
			_activePlatformsrv.Add(platformToBeCreated);
		}

		public void CreateNextPlatformrv()
		{
			Vector3 position = _lastPlatformPositionrv;
			Vector3 rotation = _lastPlatformRotationrv;
			bool dullPlatform = false;
		
			// Picks a random angle for the platform
			int randomAngleMinMax = 60 + (int)(2.5f * PlayerStatsrv.LevelHardnessMultiplierrv);

			randomAngleMinMax = Mathf.Clamp(randomAngleMinMax, 0, 360);
		
			int randomAngle = Random.Range(-randomAngleMinMax, randomAngleMinMax);
			rotation.z = randomAngle;
		
			position.z += _platformGapDistancerv;

			// This makes it so that first 4 platform in each level is in the middle
			if (  (position.z / _platformGapDistancerv) % GameManager.Instance.levelMilestone  < 4f || position.z / _platformGapDistancerv % GameManager.Instance.levelMilestone > GameManager.Instance.levelMilestone - 1)
			{
				rotation = Vector3.zero;
				dullPlatform = true;
			}
		
			// Creates the wanted platform
			CreatePlatformrv(position, rotation, dullPlatform);
		}
	}
}
