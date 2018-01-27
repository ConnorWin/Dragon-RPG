using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D offMapCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

	CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start ()
	{
		cameraRaycaster = GetComponent<CameraRaycaster> ();
        cameraRaycaster.onLayerChange += OnLayerChanged;

    }
	
	// Update is called once per frame
	void OnLayerChanged (Layer newLayer)
	{
        switch (newLayer)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(offMapCursor, cursorHotspot, CursorMode.Auto);
                break;

        }
	}
}

//TODO consider de-registering OnLayerChanged on leaving all game scenes