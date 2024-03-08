using GamePlay;
using TMPro;
using UnityEngine;

namespace UI
{
	public class GameOverUIrv : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _gameOverrv;
		[SerializeField]
		private TextMeshProUGUI _scorerv;
		[SerializeField]
		private TextMeshProUGUI _bestrv;

		private void Update()
		{
			_scorerv.text = PlayerStatsrv.Scorerv + "";
			_gameOverrv.text = (int)( 100f * PlayerStatsrv.PlatformsHoppedrv / GameManager.Instance.LevelMilestonerv) + "%" + " COMPLETED";
		}

		private void Start()
		{
			_bestrv.gameObject.SetActive(false);
		}
	
		public void ShowNewBestScorerv()
		{
			_bestrv.text = "New Best: " + PlayerStatsrv.Scorerv;
			_bestrv.gameObject.SetActive(true);
		}

		public void Restartrv()
		{
			GameManager.Instance.RestartGamerv();
		}
	}
}
