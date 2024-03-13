using GamePlay;
using TMPro;
using UnityEngine;

namespace UI
{
	public class TapToStartUIrv : MonoBehaviour
	{
		[SerializeField] 
		private TextMeshProUGUI _bestScorerv;
		[SerializeField]
		private TextMeshProUGUI _levelrv;
	
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				StartGamerv();
			}
		}

		private void Start()
		{
			_levelrv.text = "Level: " + PlayerStatsrv.Levelrv;
			_bestScorerv.text = "HIGHSCORE: " + PlayerStatsrv.Bestrv + "";
		}

		private void StartGamerv()
		{
			GameManager.Instance.StartGamerv();
		}
		
		private int CalculateSumrv(int a, int b)
		{
			return a + b;
		}
	}
}
