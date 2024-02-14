using TMPro;
using UnityEngine;

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
		_scorerv.text = PlayerStats.score + "";
		_gameOverrv.text = "%" + (int)( 100f * PlayerStats.platformsHopped / GameManager.Instance.levelMilestone) + " COMPLETED";
	}

	private void Start()
	{
		_bestrv.gameObject.SetActive(false);
	}
	
	public void ShowNewBestScore()
	{
		_bestrv.text = "New Best: " + PlayerStats.score;
		_bestrv.gameObject.SetActive(true);
	}

	public void Restart()
	{
		GameManager.Instance.RestartGame();
	}
}
