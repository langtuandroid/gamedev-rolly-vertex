using UnityEngine;

public class ColorIndicatorrv : MonoBehaviour 
{
	private HSBColor _colorrv;

	private void Start()
	{
		_colorrv = HSBColor.FromColor(GetComponent<Renderer>().sharedMaterial.GetColor("_Color"));
		transform.parent.BroadcastMessage("SetColor", _colorrv);
	}

	private void ApplyColorrv ()
	{
		GetComponent<Renderer>().sharedMaterial.SetColor ("_Color", _colorrv.ToColor());
		transform.parent.BroadcastMessage("OnColorChange", _colorrv, SendMessageOptions.DontRequireReceiver);
	}

	private void SetHuerv(float hue)
	{
		_colorrv.h = hue;
		ApplyColorrv();
    }	

	private void SetSaturationBrightnessrv(Vector2 saturatebrightnessrv)
	{
		_colorrv.s = saturatebrightnessrv.x;
		_colorrv.b = saturatebrightnessrv.y;
		ApplyColorrv();
	}
}
