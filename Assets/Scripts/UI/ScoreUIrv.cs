using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using GamePlay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ScoreUIrv : MonoBehaviour
	{
		private const string FirstMotivationText = "Nice!";
		private const string SecondMotivationText = "Great!";
		
		public static ScoreUIrv Instance;
	
		[SerializeField] 
		private TextMeshProUGUI _scorerv;
		[SerializeField]
		private TextMeshProUGUI _bestrv;
		[SerializeField]
		private TextMeshProUGUI _currentLevelrv;
		[SerializeField]
		private TextMeshProUGUI _nextLevelrv;
		[SerializeField]
		private Image _barrv;
		[SerializeField]
		private Transform _scorePopUpParentrv;
		[SerializeField]
		private GameObject _scorePopUpPrefabrv;
		[SerializeField] 
		private Transform _motivationParentrv;
		[SerializeField] 
		private TextMeshProUGUI _motivationPrefabrv;
		[SerializeField]
		private string[] _motivationTexts;
	
		private void Awake()
		{
			Instance = this;
		}
	
		private void Update()
		{
			UpdateTextFieldsrv();
			UpdateProgressBarrv();
		}
	
		private void OnDestroy()
		{
			DOTween.KillAll();
		}

		private void UpdateTextFieldsrv()
		{
			_scorerv.text = PlayerStatsrv.Scorerv + "";
			_bestrv.text = "best: " + PlayerStatsrv.Bestrv;
			_currentLevelrv.text = PlayerStatsrv.Levelrv + "";
			_nextLevelrv.text = (PlayerStatsrv.Levelrv + 1) + "";
		}

		private void UpdateProgressBarrv()
		{
			float progress = (float)PlayerStatsrv.PlatformsHoppedrv / GameManager.Instance.LevelMilestonerv;
			DOTween.To(() => _barrv.fillAmount, x => _barrv.fillAmount = x, progress, 0.25f);
		}

		public void CreateScorePopUprv()
		{
			GameObject scorePopUpGO = Instantiate(_scorePopUpPrefabrv, _scorePopUpParentrv);
			TextMeshProUGUI t = scorePopUpGO.GetComponent<TextMeshProUGUI>();
			t.text = "+" + PlayerStatsrv.Multiplierrv + "";
		
			Destroy( scorePopUpGO, 2f);
		}
	
		public void CreateMotivationPopUprv(int multiplierValue)
		{
			TextMeshProUGUI textPopupMotivation;
			textPopupMotivation = Instantiate(_motivationPrefabrv, _motivationParentrv);

			if (multiplierValue == 2)
			{
				textPopupMotivation.text = FirstMotivationText + " x" + multiplierValue;
			}
			else if (multiplierValue == 3)
			{
				textPopupMotivation.text = SecondMotivationText + " x" + multiplierValue;
			}
			else if (multiplierValue >= 4)
			{
				int randIndex = Random.Range(0, _motivationTexts.Length);
				textPopupMotivation.text = _motivationTexts[randIndex] + " x" + multiplierValue;
			}

			float randRotation = Random.Range(-15f, 15f);
			textPopupMotivation.transform.rotation = Quaternion.Euler(0,0,randRotation);
			
			Destroy(textPopupMotivation.gameObject, 3f);
		}
		
		private List<T> RemoveDuplicatesrv<T>(List<T> list)
		{
			return list.Distinct().ToList();
		}
	}
}
