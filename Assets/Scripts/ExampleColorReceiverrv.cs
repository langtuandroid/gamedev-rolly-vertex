using UnityEngine;

public class ExampleColorReceiverrv : MonoBehaviour {
	
    private Color _colorrv;

    private void OnColorChangerv(HSBColor color) 
	{
        this._colorrv = color.ToColor();
	}

    private void OnGUI()
    {
		var r = Camera.main.pixelRect;
		var rect = new Rect(r.center.x + r.height / 6 + 50, r.center.y, 100, 100);
		GUI.Label (rect, "#" + ToHex(_colorrv.r) + ToHex(_colorrv.g) + ToHex(_colorrv.b));	
    }

    private string ToHex(float n)
	{
		return ((int)(n * 255)).ToString("X").PadLeft(2, '0');
	}
}
