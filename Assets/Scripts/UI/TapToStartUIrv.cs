using TMPro;
using UnityEngine;

namespace UI
{
	public class TapToStartUI : MonoBehaviour
	{
		public TextMeshProUGUI best;
		public TextMeshProUGUI level;
	
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				StartGame();
			}

			level.text = "Level: " + PlayerStats.level;
			best.text = "HIGHSCORE: " + PlayerStats.best + "";
		}
	
		public void StartGame()
		{
			GameManager.Instance.StartGame();
		}
	}
}
