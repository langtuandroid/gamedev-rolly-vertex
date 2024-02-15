using System.Collections.Generic;
using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public TapToStartUI tapToStartUI;
	public ScoreUI scoreUI;
	[FormerlySerializedAs("gameOverUI")] public GameOverUIrv gameOverUIrv;
	public LevelTransitionUI levelTransitionUI;
	
	public PlayerMovement playerMovement;
	public GameObject platformHolder;

	public GameObject swipeToPlay;

	public int levelMilestone;
	public GameObject secondChanceObject;
	private bool secondChance = false;
	
	public float threshold;
	public float speed;
	public Platform tempPlatform;

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
		tapToStartUI.gameObject.SetActive(true);
		scoreUI.gameObject.SetActive(false);
		gameOverUIrv.gameObject.SetActive(false);
		levelTransitionUI.gameObject.SetActive(false);
        tempPlatform.platformMaterial.SetFloat("Vector1_4D600DD6", threshold);

		ColorSelection(PlayerPrefs.GetInt("Color", 0));
    }
	

	private void Update()
	{
		if (PlayerStats.platformsHopped >= levelMilestone)
		{
			NextLevel();
		}

		if (swipeToPlay.activeInHierarchy) {
			if (Input.GetMouseButtonDown(0)) {
				swipeToPlay.SetActive(false);
				playerMovement.StartMovement();
			}
		}
	}
	
	public GameObject GetPlayerHolder()
	{
		return playerMovement.gameObject;
	}

	public void StartGame()
	{
		scoreUI.gameObject.SetActive(true);
		tapToStartUI.gameObject.SetActive(false);
		playerMovement.StartMovement();
	}
	
	public void RestartGame()
	{
        tempPlatform.platformMaterial.SetFloat("Vector1_4D600DD6", threshold);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void EndGame()
	{
		scoreUI.gameObject.SetActive(false);
		gameOverUIrv.gameObject.SetActive(true);
		
		if (PlayerStats.score > PlayerStats.best)
		{
			gameOverUIrv.ShowNewBestScore();
			PlayerStats.best = PlayerStats.score;
			PlayerPrefs.SetInt("best", PlayerStats.best);
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
		PlayerStats.IncrementLevel();
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
		levelTransitionUI.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.22f);
		AudioManager.Instance.PlaySFXOneShot(3);
		LevelPassrv.Instancerv.DestroyLevelPass();
		#if (UNITY_ANDROID)
		if (AudioManager.Instance.IsVirbration)
         {Vibration.VibratePop();}
		#else
			iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
		#endif
        StartCoroutine(ChangePlatformColor());
		yield return new WaitForSeconds(0.25f);
		levelTransitionUI.gameObject.SetActive(false);
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;
		yield return new WaitForSeconds(1f);
	}

	IEnumerator ChangePlatformColor() 
	{
		var th = threshold;
        tempPlatform.platformMaterial.SetFloat("Vector1_4D600DD6", th);

		previousColor = newColor;
		newColor = colors[Random.Range(0, colors.Count)];
		while (newColor == previousColor) {
			newColor = colors[Random.Range(0, colors.Count)];
		}
		PlayerPrefs.SetInt("Color", colors.FindIndex((x) => x == previousColor));

        while (th < 300f) 
		{
			th += Time.deltaTime * speed;
            tempPlatform.platformMaterial.SetFloat("Vector1_4D600DD6", th);

			yield return null;
        }

		tempPlatform.platformMaterial.SetColor("Color_FD17D7D1", previousColor);
		tempPlatform.platformMaterial.SetColor("Color_EA86F2BE", newColor);
        tempPlatform.platformMaterial.SetFloat("Vector1_4D600DD6", threshold);

        LevelPassrv.Instancerv.PlaceNewLevelPass();
    }

	private void ColorSelection(int previousIndex) {
		previousColor = colors[previousIndex];
		newColor = colors[Random.Range(0, colors.Count)];
		while (newColor == previousColor) {
			newColor = colors[Random.Range(0, colors.Count)];
		}

		tempPlatform.platformMaterial.SetColor("Color_FD17D7D1", previousColor);
		tempPlatform.platformMaterial.SetColor("Color_EA86F2BE", newColor);
		
	}

	public void SecondChance() 
	{
		System.Action reward = () => {
			gameOverUIrv.gameObject.SetActive(false);
			scoreUI.gameObject.SetActive(true);

			var platforms = PlatformPooler.Instance.activePlatforms;
			var min = 99999f;
			var plat = platforms[0];

			for (int i = 0; i < platforms.Count; i++) {
				if (platforms[i].transform.position.z - playerMovement.gameObject.transform.position.z < min) {
					min = platforms[i].transform.position.z - playerMovement.gameObject.transform.position.z;
					plat = platforms[i];
				}
			}

			plat.transform.rotation = Quaternion.identity;
			playerMovement.gameObject.transform.rotation = Quaternion.identity;
			playerMovement.gameObject.transform.position = plat.transform.position;
			playerMovement.ball.transform.localPosition = playerMovement.jumpBottom.localPosition;

			playerMovement.ball.DOKill();

			swipeToPlay.SetActive(true);
			secondChanceObject.SetActive(false);
			secondChance = true;
		};
	}

	public void SecondChanceWithoutAd() 
	{
		gameOverUIrv.gameObject.SetActive(false);
		scoreUI.gameObject.SetActive(true);

		var platforms = PlatformPooler.Instance.activePlatforms;
		var min = 99999f;
		var plat = platforms[0];

		for (int i = 0; i < platforms.Count; i++) 
		{
			if (platforms[i].transform.position.z - playerMovement.gameObject.transform.position.z < min) {
				min = platforms[i].transform.position.z - playerMovement.gameObject.transform.position.z;
				plat = platforms[i];
			}
		}

		plat.transform.rotation = Quaternion.identity;
		playerMovement.gameObject.transform.rotation = Quaternion.identity;
		playerMovement.gameObject.transform.position = plat.transform.position;
		playerMovement.ball.transform.localPosition = playerMovement.jumpBottom.localPosition;

		playerMovement.ball.DOKill();

		swipeToPlay.SetActive(true);
		secondChanceObject.SetActive(false);
		secondChance = true;
	}
}
