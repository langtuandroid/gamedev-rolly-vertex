using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
	public static ScoreUI Instance;
	
	public TextMeshProUGUI score;
	public TextMeshProUGUI best;

	public TextMeshProUGUI currentLevel;
	public TextMeshProUGUI nextLevel;
	public Image bar;
	
	public Transform scorePopUpParent;
	public GameObject scorePopUpPrefab;

	public Transform motivationParent;
	public TextMeshProUGUI motivationPrefab;
	
	public string firstMotivationText = "Nice!";
	public string secondMotivationText = "Great!";
	public string[] motivationTexts;
	
	private void Awake()
	{
		Instance = this;
	}
	
	private void Update()
	{
		UpdateTextFields();
		UpdateProgressBar();
	}
	
	private void OnDestroy()
	{
		DOTween.KillAll();
	}

	private void UpdateTextFields()
	{
		score.text = PlayerStats.score + "";
		best.text = "best: " + PlayerStats.best;
		currentLevel.text = PlayerStats.level + "";
		nextLevel.text = (PlayerStats.level + 1) + "";
	}

	private void UpdateProgressBar()
	{
		float progress = (float)PlayerStats.platformsHopped / GameManager.Instance.levelMilestone;
		DOTween.To(() => bar.fillAmount, x => bar.fillAmount = x, progress, 0.25f);
	}

	public void CreateScorePopUp()
	{
		GameObject scorePopUpGO = Instantiate(scorePopUpPrefab, scorePopUpParent);
		TextMeshProUGUI t = scorePopUpGO.GetComponent<TextMeshProUGUI>();
		t.text = "+" + PlayerStats.multiplier + "";
		
		Destroy( scorePopUpGO, 2f);
	}
	
	public void CreateMotivationPopUp(int multiplier)
	{
		TextMeshProUGUI t;
		t = Instantiate(motivationPrefab, motivationParent);

		if (multiplier == 2)
		{
			t.text = firstMotivationText + " x" + multiplier;
		}
		else if (multiplier == 3)
		{
			t.text = secondMotivationText + " x" + multiplier;
		}
		else if (multiplier >= 4)
		{
			int randIndex = Random.Range(0, motivationTexts.Length);
			t.text = motivationTexts[randIndex] + " x" + multiplier;
		}

		float randRotation = Random.Range(-15f, 15f);
		t.transform.rotation = Quaternion.Euler(0,0,randRotation);
		
		
		Destroy(t.gameObject, 3f);
	}
}
