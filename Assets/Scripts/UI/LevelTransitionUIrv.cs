using TMPro;
using UnityEngine;

namespace UI
{
	public class LevelTransitionUI : MonoBehaviour
	{
		[SerializeField]
		private  TextMeshProUGUI _levelTransitionTextrv;

		void Update()
		{
			_levelTransitionTextrv.text = "Vortex " + PlayerStats.level;
		}
	}
}
