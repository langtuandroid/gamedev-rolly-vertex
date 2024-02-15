using UnityEngine;

namespace GamePlay
{
	public class CameraFollowrv : MonoBehaviour
	{
		[SerializeField]
		private float _fixedXrv;
		[SerializeField]
		private float _fixedYrv;
		[SerializeField]
		private float _offsetZrv;
		[SerializeField]
		private Transform _targetrv;
	
		private Vector3 _positionrv;

		public float FixedX
		{
			get => _fixedXrv;
			set => _fixedXrv = value;
		}

		public float FixedY
		{
			get => _fixedYrv;
			set => _fixedYrv = value;
		}

		public float OffsetZ
		{
			get => _offsetZrv;
			set => _offsetZrv = value;
		}

		private void Update () 
		{
			_positionrv = _targetrv.position;
			_positionrv.x = FixedX;
			_positionrv.y = FixedY;
			_positionrv.z += OffsetZ;
			transform.position = _positionrv;
		}
	}
}
