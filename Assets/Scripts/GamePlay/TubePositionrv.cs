using UnityEngine;

namespace GamePlay
{
	public class TubePositionrv : MonoBehaviour
	{
		private GameObject _playerHolderrv;
		private bool _destroyedrv;
	
		private void Start()
		{
			_playerHolderrv = GameManager.Instance.GetPlayerHolderrv();
		}
	
		private void Update () 
		{
			if (!_destroyedrv && transform.position.z < _playerHolderrv.transform.position.z + 20f)
			{
				TubePoolerrv.Instancerv.DestroyTuberv(gameObject,2f);
				TubePoolerrv.Instancerv.CreateNextPlatformrv();
				_destroyedrv = true;
			}
		}

		private void OnEnable()
		{
			_destroyedrv = false;
		}
	}
}
