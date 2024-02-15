using UnityEngine;

namespace Colors
{
	public class ExampleColorReceiverrv : MonoBehaviour
	{
		private Color _colorrv;

		private void OnColorChangerv(HSBColor color) 
		{
			_colorrv = color.ToColor();
		}

		private void OnGUI()
		{
			var rectrv = Camera.main.pixelRect;
			var rect = new Rect(rectrv.center.x + rectrv.height / 6 + 50, rectrv.center.y, 100, 100);
			GUI.Label (rect, "#" + ToHex(_colorrv.r) + ToHex(_colorrv.g) + ToHex(_colorrv.b));	
		}

		private string ToHex(float numberrv)
		{
			return ((int)(numberrv * 255)).ToString("X").PadLeft(2, '0');
		}
	}
}
