using UnityEngine;

public class ColorHuePickerrv : MonoBehaviour
{
	private void SetColor(HSBColor color)
	{
		SendMessage("SetDragPoint", new Vector3(color.h, 0, 0));
	}	

    private void OnDrag(Vector3 point)
    {
		transform.parent.BroadcastMessage("SetHue", point.x);
    }
}
