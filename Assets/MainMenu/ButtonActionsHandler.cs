using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D defaultCursor = null;
    public Texture2D hoverCursor;
    public Vector2 hotspot = Vector2.zero;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}