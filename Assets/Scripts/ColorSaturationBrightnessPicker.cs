using UnityEngine;


public class ColorSaturationBrightnessPicker : MonoBehaviour
{
	public Material backgroundMaterialrv;

	private void SetColor(HSBColor color)
	{
		backgroundMaterialrv.SetColor("_Color", new HSBColor(color.h, 1, 1).ToColor());
		SendMessage("SetDragPoint", new Vector3(color.s, color.b, 0));
	}

	private void OnDrag(Vector3 point)
    {
		transform.parent.BroadcastMessage("SetSaturationBrightness", new Vector2(point.x, point.y));
    }

    private void SetHuerv(float hue)
    {
		backgroundMaterialrv.SetColor("_Color", new HSBColor(hue, 1, 1).ToColor());
    }	
}
