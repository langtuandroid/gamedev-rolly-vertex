using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GamePlay
{
	public class Platformrv : MonoBehaviour
	{
		private bool _canMoverv;
		private bool _hasPerfectrv;
		[SerializeField]
		private Transform _perfectrv;
		[SerializeField]
		private Transform _perfectEffectrv;
		[SerializeField]
		private Transform _scorervv;
		[SerializeField]
		private TextMeshProUGUI _scoreTextrv;
		[SerializeField]
		private Material _platformMaterialrv;
		[SerializeField]
		private Material _tempPlatformMaterialrv;
		[SerializeField]
		private Color _defaultColorrv;
		[SerializeField]
		private Color _perfectColorrv;
		[SerializeField]
		private MeshRenderer _platformRendererrv;
		[SerializeField]
		private Transform[] _platformSizesrv;

		public bool HasPerfectrv
		{
			get => _hasPerfectrv;
			set => _hasPerfectrv = value;
		}

		public Material PlatformMaterialrv
		{
			get => _platformMaterialrv;
			set => _platformMaterialrv = value;
		}

		public void SetPerfectrv()
		{
			HasPerfectrv = true;
			_perfectrv.gameObject.SetActive(true);
		}

		public void ResetPerfectrv()
		{
			HasPerfectrv = false;
			_perfectrv.gameObject.SetActive(false);
			_perfectEffectrv.gameObject.SetActive(false);
		}

		public void ShowScorerv( int _score )
		{
			_scoreTextrv.text = "+" + _score;
			_scorervv.gameObject.SetActive(true);
		}

		public void HideScorerv()
		{
			_scorervv.gameObject.SetActive(false);
		}

		public void PerfectColorrv()
		{
			_tempPlatformMaterialrv.color = _defaultColorrv;
			_platformRendererrv.material = _tempPlatformMaterialrv;
			_tempPlatformMaterialrv.DOColor(_perfectColorrv, 0.125f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
			{
				_platformRendererrv.material = PlatformMaterialrv;
			});
		}

		public void SetMoveablerv()
		{
			_canMoverv = true;

			Vector3 newRotation = transform.rotation.eulerAngles;
			newRotation.z += 30f + PlayerStatsrv.LevelHardnessMultiplierrv / 2f;
			transform.DORotate(newRotation, 1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
		}

		public void ResetMoveablerv()
		{
			_canMoverv = false;
			transform.DOKill();
		}

		public void ResetEverythingrv()
		{
			HideScorerv();
			ResetMoveablerv();
			ResetPerfectrv();
			ResetSizerv();
		}

		public void RandomSizerv()
		{
			int randNumber = Random.Range(0, _platformSizesrv.Length);
		
			if (PlayerStatsrv.Levelrv >= 100f)
			{
				randNumber = Random.Range(3, _platformSizesrv.Length);
			}
		
			if (PlayerStatsrv.Levelrv < 50f)
			{
				randNumber = Random.Range(3,4);
			}
		
			if (PlayerStatsrv.Levelrv < 20f)
			{
				randNumber = Random.Range(2,3);
			}
			if (PlayerStatsrv.Levelrv < 10f)
			{
				randNumber = Random.Range(1,2);
		
			}
		
			if (PlayerStatsrv.Levelrv < 5f)
			{
				randNumber = Random.Range(0, 1);
			}
		
			_platformSizesrv[3].gameObject.SetActive(false);
		
			_platformSizesrv[randNumber].gameObject.SetActive(true);

		}

		public void ResetSizerv()
		{
			for (int i = 0; i < _platformSizesrv.Length; i++)
			{
				_platformSizesrv[i].gameObject.SetActive(false);
			}
		
			_platformSizesrv[3].gameObject.SetActive(true);
		}

		public void PlayPerfectEffectrv()
		{
			_perfectEffectrv.gameObject.SetActive(true);
			PerfectColorrv();
		}
	}
}
