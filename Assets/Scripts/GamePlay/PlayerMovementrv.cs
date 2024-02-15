using Audio;
using UnityEngine;
using DG.Tweening;
using GamePlay;
using UI;

public class PlayerMovement : MonoBehaviour
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

	public void StartMovement()
	{	
		touchDown();

		_doMovementrv = true;
		Jump();
		transform.DOMoveZ(transform.position.z + 25f, 0.44f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).OnStepComplete(() =>
		{
			var pos = transform.position;
			pos.z = (int) pos.z;
			transform.position = pos;
		});
	}
	
	private void Jump()
	{
		Ballrv.DOLocalMoveY(_jumpToprv.position.y, 0.22f).SetLoops(-1, LoopType.Yoyo).OnStepComplete(() =>
		{

			if (_secondSteprv)
			{
				//RaycastHit hit;
				//if (Physics.Raycast(rayHitPoint.position, -rayHitPoint.up, out hit, 100f, LayerMask.GetMask("Perfect")))
				//{
				//	Debug.Log("hit Perfect");
				//	
				//	PlayerStats.IncrementMultiplier();
				//}

				// executes if hit a platform
				RaycastHit hit;
				if (Physics.Raycast(_rayHitPointrv.position, -_rayHitPointrv.up, out hit, 100f))
				{	
					hit.transform.parent.DOScale(new Vector3(1.5f, 1, 1.5f), 0.1f).SetLoops(2,LoopType.Yoyo);

					Platformrv currentPlatformrv = hit.transform.GetComponentInParent<Platformrv>();
					AudioManager.Instance.PlaySFXOneShot(2);
					// executes if hit a perfect
					if ( currentPlatformrv.HasPerfectrv )
					{
						if (hit.transform.CompareTag("Perfect"))
						{
							PlayerStats.IncrementMultiplier();
							ScoreUI.Instance.CreateMotivationPopUp(PlayerStats.multiplier);
							//currentPlatform.PerfectColor();
							currentPlatformrv.PlayPerfectEffectrv();
							AudioManager.Instance.PlaySFXOneShot(4);
						}
						else
						{
							PlayerStats.ResetMultiplier();
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
					currentPlatformrv.ShowScorerv(PlayerStats.multiplier * PlayerStats.level);
					
					
					PlayerStats.IncrementPlatformsHopped();
					PlayerStats.IncrementScore();
					
					PlatformPoolerrv.Instancerv.CreateNextPlatformrv();
					PlatformPoolerrv.Instancerv.DestroyPlatformrv(hit.transform.gameObject, 1f);
					
				}
				else
				{
					EndGame();
				}
			}

			_secondSteprv = !_secondSteprv;
		});
	}

	private void EndGame()
	{
		AudioManager.Instance.PlaySFXOneShot(5);
		DOTween.KillAll();
		Ballrv.DOLocalMoveY(-20f, 1f);
		_doMovementrv = false;
		GameManager.Instance.EndGame();
		
	}
	
	private void Update()
	{
		if (!_doMovementrv)
		{
			return;
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			touchDown();
		}

		if (Input.GetMouseButtonUp(0))
		{
			touchUp();
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
	
	private void touchDown()
	{
		_firstPosrv = Input.mousePosition;
		_firstPosrv.x -= (float)Screen.width / 2;
		_firstPosrv.x = _firstPosrv.x / Screen.width;
		
		_isTouchedrv = true;
	}

	private void touchUp()
	{
		_isTouchedrv = false;
	}
}
