using UnityEngine;

namespace GamePlay
{
	public class Draggablerv : MonoBehaviour
	{
		[SerializeField]
		private bool _fixXrv;
		[SerializeField]
		private bool _fixYrv;
		[SerializeField]
		private Transform _thumbrv;	
		private bool _draggingrv;

		private void FixedUpdate()
		{
			if (Input.GetMouseButtonDown(0))
			{
				_draggingrv = false;
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (GetComponent<Collider>().Raycast(ray, out hit, 100)) {
					_draggingrv = true;
				}
			}
			if (Input.GetMouseButtonUp(0)) _draggingrv = false;
			if (_draggingrv && Input.GetMouseButton(0)) 
			{
				var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				point = GetComponent<Collider>().ClosestPointOnBounds(point);
				SetThumbPositionrv(point);
				SendMessage("OnDrag", Vector3.one - (_thumbrv.position - GetComponent<Collider>().bounds.min) / GetComponent<Collider>().bounds.size.x);
			}
		}

		private void SetDragPointrv(Vector3 point)
		{
			point = (Vector3.one - point) * GetComponent<Collider>().bounds.size.x + GetComponent<Collider>().bounds.min;
			SetThumbPositionrv(point);
		}

		private void SetThumbPositionrv(Vector3 point)
		{
			_thumbrv.position = new Vector3(_fixXrv ? _thumbrv.position.x : point.x, _fixYrv ? _thumbrv.position.y : point.y, _thumbrv.position.z);
		}
	}
}
