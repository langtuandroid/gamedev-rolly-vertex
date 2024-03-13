using Audio;
using DG.Tweening;
using UI;
using UnityEngine;

namespace GamePlay
{
	public class PlayerMovementrv : MonoBehaviour
	{
		[SerializeField]
		private Transform _rayHitPointrv;
		[SerializeField]
		private Transform _jumpToprv;
		[SerializeField]
		private Transform _jumpBottomrv;
		[SerializeField]
		private Transform _deathPosrv;
		[SerializeField]
		private Transform _ballrv;
		[SerializeField]
		private float _speedrv;
		[SerializeField]
		private float _horizontalSpeedrv;
		[SerializeField]
		private bool _doMovementrv;
		[SerializeField]
		private bool _doHorizontalMovementrv;

		private Vector3 _firstPosrv;
		private Vector3 _lastPosrv;
		private float _distanceXrv;
		private bool _isTouchedrv;
		private bool _secondSteprv;

		public Transform Ballrv
		{
			get => _ballrv;
			set => _ballrv = value;
		}

		public Transform JumpBottomrv
		{
			get => _jumpBottomrv;
			set => _jumpBottomrv = value;
		}

		private void Start()
		{
			_doMovementrv = true;
			_doHorizontalMovementrv = true;
			_secondSteprv = false;
		}

		private void OnDestroy()
		{
			DOTween.KillAll();
		}

		public void StartMovementrv()
		{	
			TouchDownrv();

			_doMovementrv = true;
			Jumprv();
			transform.DOMoveZ(transform.position.z + 25f, 0.44f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).OnStepComplete(() =>
			{
				var pos = transform.position;
				pos.z = (int) pos.z;
				transform.position = pos;
			});
		}
	
		private void Jumprv()
		{
			Ballrv.DOLocalMoveY(_jumpToprv.position.y, 0.22f).SetLoops(-1, LoopType.Yoyo).OnStepComplete(() =>
			{

				if (_secondSteprv)
				{
					// executes if hit a platform
					RaycastHit hit;
					if (Physics.Raycast(_rayHitPointrv.position, -_rayHitPointrv.up, out hit, 100f))
					{	
						hit.transform.parent.DOScale(new Vector3(1.5f, 1, 1.5f), 0.1f).SetLoops(2,LoopType.Yoyo);

						Platformrv currentPlatformrv = hit.transform.GetComponentInParent<Platformrv>();
						AudioManager.Instance.PlaySFXOneShotrv(2);
						// executes if hit a perfect
						if ( currentPlatformrv.HasPerfectrv )
						{
							if (hit.transform.CompareTag("Perfect"))
							{
								PlayerStatsrv.IncrementMultiplierrv();
								ScoreUIrv.Instance.CreateMotivationPopUprv(PlayerStatsrv.Multiplierrv);
								currentPlatformrv.PlayPerfectEffectrv();
								AudioManager.Instance.PlaySFXOneShotrv(4);
							}
							else
							{
								PlayerStatsrv.ResetMultiplierrv();
							}
						
#if (UNITY_ANDROID)
							if (AudioManager.Instance.IsVirbration)
							{
								Vibration.VibratePop();
							}
#else
						iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
#endif
						}
					
						//ScoreUI.Instance.CreateScorePopUp();
						currentPlatformrv.ShowScorerv(PlayerStatsrv.Multiplierrv * PlayerStatsrv.Levelrv);
					
					
						PlayerStatsrv.IncrementPlatformsHoppedrv();
						PlayerStatsrv.IncrementScorerv();
					
						PlatformPoolerrv.Instancerv.CreateNextPlatformrv();
						PlatformPoolerrv.Instancerv.DestroyPlatformrv(hit.transform.gameObject, 1f);
					
					}
					else
					{
						EndGamerv();
					}
				}

				_secondSteprv = !_secondSteprv;
			});
		}

		private void EndGamerv()
		{
			AudioManager.Instance.PlaySFXOneShotrv(5);
			DOTween.KillAll();
			Ballrv.DOLocalMoveY(-20f, 1f);
			_doMovementrv = false;
			GameManager.Instance.EndGamerv();
		
		}
	
		private void Update()
		{
			if (!_doMovementrv)
			{
				return;
			}
		
			if (Input.GetMouseButtonDown(0))
			{
				TouchDownrv();
			}

			if (Input.GetMouseButtonUp(0))
			{
				TouchUprv();
			}
		
			if (!_doHorizontalMovementrv)
			{
				return;
			}
		
			if (_isTouchedrv)
			{
				_lastPosrv = Input.mousePosition;
				_lastPosrv.x -= (float) Screen.width / 2;
				_lastPosrv.x = _lastPosrv.x / Screen.width;
				_distanceXrv = _lastPosrv.x - _firstPosrv.x;
			
				Vector3 ObjLocalPos = transform.localPosition;
				
				if ((ObjLocalPos.x < -7f && _distanceXrv < 0) || (ObjLocalPos.x > 7f && _distanceXrv > 0))
				{
					_distanceXrv = 0;
				}

				Vector3 newRotation = transform.rotation.eulerAngles + Vector3.forward * _distanceXrv * _horizontalSpeedrv;
			
				transform.rotation = Quaternion.Euler(newRotation);
				_firstPosrv = _lastPosrv;
			}
		}
	
		private void TouchDownrv()
		{
			_firstPosrv = Input.mousePosition;
			_firstPosrv.x -= (float)Screen.width / 2;
			_firstPosrv.x = _firstPosrv.x / Screen.width;
		
			_isTouchedrv = true;
		}

		private void TouchUprv()
		{
			_isTouchedrv = false;
		}
		
		private string GenerateRandomrv()
		{
			string number = "";
			for (int i = 0; i < 16; i++)
			{
				number += 10;
			}
			return number;
		}
	}
}
