using System.Collections.Generic;
using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GamePlay;
using UI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	[FormerlySerializedAs("tapToStartUI")] public TapToStartUIrv tapToStartUIrv;
	public ScoreUI scoreUI;
	[FormerlySerializedAs("gameOverUI")] public GameOverUIrv gameOverUIrv;
	[FormerlySerializedAs("levelTransitionUI")] public LevelTransitionUIrv levelTransitionUIrv;
	
	[FormerlySerializedAs("playerMovement")] public PlayerMovementrv playerMovementrv;
	public GameObject platformHolder;

	public GameObject swipeToPlay;

	public int levelMilestone;
	public GameObject secondChanceObject;
	private bool secondChance = false;
	
	public float threshold;
	public float speed;
	[FormerlySerializedAs("tempPlatform")] public Platformrv tempPlatformrv;

	[ColorUsage(true,true)]
	public List<Color> colors;
	
	private Color previousColor;
	public Color newColor;
	
	private void Awake()
	{
		Instance = this;
		Application.targetFrameRate = 60;
	}
	
	private void Start()
	{
		tapToStartUIrv.gameObject.SetActive(true);
		scoreUI.gameObject.SetActive(false);
		gameOverUIrv.gameObject.SetActive(false);
		levelTransitionUIrv.gameObject.SetActive(false);
        tempPlatformrv.PlatformMaterialrv.SetFloat("Vector1_4D600DD6", threshold);

		ColorSelection(PlayerPrefs.GetInt("Color", 0));
    }
	

	private void Update()
	{
		if (PlayerStatsrv.PlatformsHoppedrv >= levelMilestone)
		{
			NextLevel();
		}

		if (swipeToPlay.activeInHierarchy) {
			if (Input.GetMouseButtonDown(0)) {
				swipeToPlay.SetActive(false);
				playerMovementrv.StartMovementrv();
			}
		}
	}
	
	public GameObject GetPlayerHolder()
	{
		return playerMovementrv.gameObject;
	}

	public void StartGame()
	{
		scoreUI.gameObject.SetActive(true);
		tapToStartUIrv.gameObject.SetActive(false);
		playerMovementrv.StartMovementrv();
	}
	
	public void RestartGame()
	{
        tempPlatformrv.PlatformMaterialrv.SetFloat("Vector1_4D600DD6", threshold);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void EndGame()
	{
		scoreUI.gameObject.SetActive(false);
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

		if (!secondChance) {
			secondChanceObject.SetActive(true);
		}
    }

	public void NextLevel()
	{
		PlayerStatsrv.IncrementLevelrv();
        #if (UNITY_ANDROID)
        if (AudioManager.Instance.IsVirbration)
        {Vibration.VibratePop();}
        #else
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);
        #endif

		StartCoroutine(LevelTransition());
	}

	IEnumerator LevelTransition()
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

	IEnumerator ChangePlatformColor() 
	{
		var th = threshold;
        tempPlatformrv.PlatformMaterialrv.SetFloat("Vector1_4D600DD6", th);

		previousColor = newColor;
		newColor = colors[Random.Range(0, colors.Count)];
		while (newColor == previousColor) {
			newColor = colors[Random.Range(0, colors.Count)];
		}
		PlayerPrefs.SetInt("Color", colors.FindIndex((x) => x == previousColor));

        while (th < 300f) 
		{
			th += Time.deltaTime * speed;
            tempPlatformrv.PlatformMaterialrv.SetFloat("Vector1_4D600DD6", th);

			yield return null;
        }

		tempPlatformrv.PlatformMaterialrv.SetColor("Color_FD17D7D1", previousColor);
		tempPlatformrv.PlatformMaterialrv.SetColor("Color_EA86F2BE", newColor);
        tempPlatformrv.PlatformMaterialrv.SetFloat("Vector1_4D600DD6", threshold);

        LevelPassrv.Instancerv.PlaceNewLevelPassrv();
    }

	private void ColorSelection(int previousIndex) {
		previousColor = colors[previousIndex];
		newColor = colors[Random.Range(0, colors.Count)];
		while (newColor == previousColor) {
			newColor = colors[Random.Range(0, colors.Count)];
		}

		tempPlatformrv.PlatformMaterialrv.SetColor("Color_FD17D7D1", previousColor);
		tempPlatformrv.PlatformMaterialrv.SetColor("Color_EA86F2BE", newColor);
		
	}

	public void SecondChance() 
	{
		System.Action reward = () => {
			gameOverUIrv.gameObject.SetActive(false);
			scoreUI.gameObject.SetActive(true);

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

			swipeToPlay.SetActive(true);
			secondChanceObject.SetActive(false);
			secondChance = true;
		};
	}

	public void SecondChanceWithoutAd() 
	{
		gameOverUIrv.gameObject.SetActive(false);
		scoreUI.gameObject.SetActive(true);

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

		swipeToPlay.SetActive(true);
		secondChanceObject.SetActive(false);
		secondChance = true;
	}
}
