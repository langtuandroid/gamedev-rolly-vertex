using GamePlay;
using TMPro;
using UnityEngine;

namespace UI
{
	public class TapToStartUIrv : MonoBehaviour
	{
		public TextMeshProUGUI best;
		public TextMeshProUGUI level;
	
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				StartGame();
			}
		}

		private void Start()
		{
			level.text = "Level: " + PlayerStatsrv.Levelrv;
			best.text = "HIGHSCORE: " + PlayerStatsrv.Bestrv + "";
		}

		public void StartGame()
		{
			GameManager.Instance.StartGamerv();
		}
	}
}
