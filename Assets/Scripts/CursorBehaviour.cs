using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    public Texture2D cursorTexture;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, new Vector2(32,32), CursorMode.ForceSoftware);
    }
    private void Update()
    {
        if(Time.timeScale < 1.0f)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(cursorTexture, new Vector2(32, 32), CursorMode.ForceSoftware);
    }
}
 