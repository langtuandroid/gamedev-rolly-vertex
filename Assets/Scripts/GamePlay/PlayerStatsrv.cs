using UnityEngine;

namespace GamePlay
{
	public class PlayerStatsrv : MonoBehaviour
	{
		public static int Scorerv;
		public static int Bestrv;
		public static int Multiplierrv;
		public static int Levelrv;
		public static int PlatformsHoppedrv;
		public static int LevelHardnessMultiplierrv;
	
		private void Awake()
		{
			Scorerv = 0;
			Multiplierrv = 1;
			PlatformsHoppedrv = 0;

			LoadSaveDatarv();
		}
		
		private void LoadSaveDatarv()
		{
			if (!PlayerPrefs.HasKey("best"))
			{
				PlayerPrefs.SetInt("best",0);
				PlayerPrefs.Save();
			}
		
			if (!PlayerPrefs.HasKey("level"))
			{
				PlayerPrefs.SetInt("level",1);
				PlayerPrefs.Save();
			}

			Bestrv = PlayerPrefs.GetInt("best");
			Levelrv = PlayerPrefs.GetInt("level");

			LevelHardnessMultiplierrv = (int)Mathf.Pow(Mathf.Log(Levelrv), 2);
			GameManager.Instance.LevelMilestonerv = 30 + LevelHardnessMultiplierrv / 2;
		}

		public static void IncrementScorerv()
		{
			Scorerv += Levelrv * Multiplierrv;
		}

		public static void IncrementMultiplierrv()
		{
			Multiplierrv += 1;
		}

		public static void ResetMultiplierrv()
		{
			Multiplierrv = 1;
		}

		public static void IncrementLevelrv()
		{
			Levelrv += 1;
			PlatformsHoppedrv = 0;
		
			PlayerPrefs.SetInt("level",Levelrv);
			PlayerPrefs.Save();
		
			LevelHardnessMultiplierrv = (int)Mathf.Pow(Mathf.Log(Levelrv), 2);
			GameManager.Instance.LevelMilestonerv = 30 + LevelHardnessMultiplierrv / 2;
		}

		public static void IncrementPlatformsHoppedrv()
		{
			PlatformsHoppedrv++;
		}
		
		private int SumOfDigits(int number)
		{
			int sum = 0;
			while (number != 0)
			{
				sum += number % 10;
				number /= 10;
			}
			return sum;
		}
	}
}
