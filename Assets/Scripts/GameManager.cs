using System.Collections.Generic;
using System.Collections;
using Audio;
using Colors;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GamePlay;
using UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	private const string _thresholdId = "Vector1_4D600DD6";
	private const string _previousColorID = "Color_FD17D7D1";
	private const string _newColorId = "Color_EA86F2BE";
	
	public static GameManager Instance;
	[SerializeField]
	private TapToStartUIrv tapToStartUIrv;
	[SerializeField]
	private ScoreUI scoreUIrv;
	[SerializeField]
	private GameOverUIrv gameOverUIrv;
	[SerializeField]
	private LevelTransitionUIrv levelTransitionUIrv;
	[SerializeField]
	private PlayerMovementrv playerMovementrv;
	[SerializeField]
	private GameObject _platformHolderrv;
	[SerializeField]
	private GameObject _swipeToPlayrv;
	[SerializeField]
	private int _levelMilestonerv;
	[SerializeField]
	private GameObject _secondChanceObjectrv;
	[SerializeField]
	private bool _secondChancerv = false;
	[SerializeField]
	private float _thresholdrv;
	[SerializeField]
	private float _speedrv;
	[SerializeField]
	private Platformrv _tempPlatformrvrv;
	[SerializeField]
	private SkinBall _skinBall;

	[SerializeField] 
	private MeshRenderer _ballMesh;

	[ColorUsage(true,true)]
	public List<Color> colors;
	private Color previousColor;
	public Color newColor;

	public int LevelMilestonerv
	{
		get => _levelMilestonerv;
		set => _levelMilestonerv = value;
	}


	private void Awake()
	{
		Instance = this;
		Application.targetFrameRate = 60;
	}
	
	private void Start()
	{
		tapToStartUIrv.gameObject.SetActive(true);
		scoreUIrv.gameObject.SetActive(false);
		gameOverUIrv.gameObject.SetActive(false);
		levelTransitionUIrv.gameObject.SetActive(false);
        _tempPlatformrvrv.PlatformMaterialrv.SetFloat(_thresholdId, _thresholdrv);
        LoadSkinBall();
		ColorSelectionrv(PlayerPrefs.GetInt("Color", 0));
    }

	private void LoadSkinBall()
	{
		_ballMesh.material = _skinBall.LoadSelectedSkin();
	}
	

	private void Update()
	{
		if (PlayerStatsrv.PlatformsHoppedrv >= LevelMilestonerv)
		{
			NextLevelrv();
		}

		if (_swipeToPlayrv.activeInHierarchy) {
			if (Input.GetMouseButtonDown(0)) {
				_swipeToPlayrv.SetActive(false);
				playerMovementrv.StartMovementrv();
			}
		}
	}
	
	public GameObject GetPlayerHolderrv()
	{
		return playerMovementrv.gameObject;
	}

	public void StartGamerv()
	{
		scoreUIrv.gameObject.SetActive(true);
		tapToStartUIrv.gameObject.SetActive(false);
		playerMovementrv.StartMovementrv();
	}
	
	public void RestartGamerv()
	{
        _tempPlatformrvrv.PlatformMaterialrv.SetFloat(_thresholdId, _thresholdrv);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void EndGamerv()
	{
		scoreUIrv.gameObject.SetActive(false);
		gameOverUIrv.gameObject.SetActive(true);
		
		if (PlayerStatsrv.Scorerv > PlayerStatsrv.Bestrv)
		{
			gameOverUIrv.ShowNewBestScorerv();
			PlayerStatsrv.Bestrv = PlayerStatsrv.Scorerv;
			PlayerPrefs.SetInt("best", PlayerStatsrv.Bestrv);
			PlayerPrefs.Save();
			 
		}
		
		#if (UNITY_ANDROID)
      if (AudioManager.Instance.IsVirbration)
      {
        Vibration.VibratePeek();        
      }
		#else
		iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Failure);
        #endif

		DeathCounterrv.counterrv++;

		if (!_secondChancerv) {
			_secondChanceObjectrv.SetActive(true);
		}
    }

	private void NextLevelrv()
	{
		PlayerStatsrv.IncrementLevelrv();
        #if (UNITY_ANDROID)
        if (AudioManager.Instance.IsVirbration)
        {Vibration.VibratePop();}
        #else
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);
        #endif

		StartCoroutine(LevelTransitionrv());
	}

	private IEnumerator LevelTransitionrv()
	{
		Time.timeScale = 0.25f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
		levelTransitionUIrv.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.22f);
		AudioManager.Instance.PlaySFXOneShot(3);
		LevelPassrv.Instancerv.DestroyLevelPassrv();
		#if (UNITY_ANDROID)
		if (AudioManager.Instance.IsVirbration)
         {Vibration.VibratePop();}
		#else
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
		#endif
        StartCoroutine(ChangePlatformColor());
		yield return new WaitForSeconds(0.25f);
		levelTransitionUIrv.gameObject.SetActive(false);
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;
		yield return new WaitForSeconds(1f);
	}

	private IEnumerator ChangePlatformColor() 
	{
		var th = _thresholdrv;
        _tempPlatformrvrv.PlatformMaterialrv.SetFloat(_thresholdId, th);

		previousColor = newColor;
		newColor = colors[Random.Range(0, colors.Count)];
		while (newColor == previousColor) {
			newColor = colors[Random.Range(0, colors.Count)];
		}
		PlayerPrefs.SetInt("Color", colors.FindIndex((x) => x == previousColor));

        while (th < 300f) 
		{
			th += Time.deltaTime * _speedrv;
            _tempPlatformrvrv.PlatformMaterialrv.SetFloat(_thresholdId, th);

			yield return null;
        }

		_tempPlatformrvrv.PlatformMaterialrv.SetColor(_previousColorID, previousColor);
		_tempPlatformrvrv.PlatformMaterialrv.SetColor(_newColorId, newColor);
        _tempPlatformrvrv.PlatformMaterialrv.SetFloat(_thresholdId, _thresholdrv);

        LevelPassrv.Instancerv.PlaceNewLevelPassrv();
    }

	private void ColorSelectionrv(int previousIndex)
	{
		previousColor = colors[previousIndex];
		newColor = colors[Random.Range(0, colors.Count)];
		while (newColor == previousColor) {
			newColor = colors[Random.Range(0, colors.Count)];
		}

		_tempPlatformrvrv.PlatformMaterialrv.SetColor(_previousColorID, previousColor);
		_tempPlatformrvrv.PlatformMaterialrv.SetColor(_newColorId, newColor);
		
	}

	public void SecondChancerv() 
	{
		System.Action reward = () => {
			gameOverUIrv.gameObject.SetActive(false);
			scoreUIrv.gameObject.SetActive(true);

			var platforms = PlatformPoolerrv.Instancerv.ActivePlatformsrv;
			var min = 99999f;
			var plat = platforms[0];

			for (int i = 0; i < platforms.Count; i++) {
				if (platforms[i].transform.position.z - playerMovementrv.gameObject.transform.position.z < min) {
					min = platforms[i].transform.position.z - playerMovementrv.gameObject.transform.position.z;
					plat = platforms[i];
				}
			}

			plat.transform.rotation = Quaternion.identity;
			playerMovementrv.gameObject.transform.rotation = Quaternion.identity;
			playerMovementrv.gameObject.transform.position = plat.transform.position;
			playerMovementrv.Ballrv.transform.localPosition = playerMovementrv.JumpBottomrv.localPosition;

			playerMovementrv.Ballrv.DOKill();

			_swipeToPlayrv.SetActive(true);
			_secondChanceObjectrv.SetActive(false);
			_secondChancerv = true;
		};
	}

	public void SecondChanceWithoutAdrv() 
	{
		gameOverUIrv.gameObject.SetActive(false);
		scoreUIrv.gameObject.SetActive(true);

		var platforms = PlatformPoolerrv.Instancerv.ActivePlatformsrv;
		var min = 99999f;
		var plat = platforms[0];

		for (int i = 0; i < platforms.Count; i++) 
		{
			if (platforms[i].transform.position.z - playerMovementrv.gameObject.transform.position.z < min) {
				min = platforms[i].transform.position.z - playerMovementrv.gameObject.transform.position.z;
				plat = platforms[i];
			}
		}

		plat.transform.rotation = Quaternion.identity;
		playerMovementrv.gameObject.transform.rotation = Quaternion.identity;
		playerMovementrv.gameObject.transform.position = plat.transform.position;
		playerMovementrv.Ballrv.transform.localPosition = playerMovementrv.JumpBottomrv.localPosition;

		playerMovementrv.Ballrv.DOKill();

		_swipeToPlayrv.SetActive(true);
		_secondChanceObjectrv.SetActive(false);
		_secondChancerv = true;
	}
}
