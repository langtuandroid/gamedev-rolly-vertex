﻿using System.Linq;
using GamePlay;
using TMPro;
using UnityEngine;

namespace UI
{
	public class LevelTransitionUIrv : MonoBehaviour
	{
		[SerializeField]
		private  TextMeshProUGUI _levelTransitionTextrv;

		private void OnEnable()
		{
			_levelTransitionTextrv.text = "Vortex " + PlayerStatsrv.Levelrv;
		}
		
		private int CountOccurrencesrv(string str, char target)
		{
			return str.Count(c => c == target);
		}
	}
}
