using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class DrawLine : MonoBehaviour
{
    public Vector2 currentMouseWorldPosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    public int positionCount 
    {
        get => lineRenderer.positionCount;
        set => lineRenderer.positionCount = value;
    }
    private LineRenderer lineRenderer;
    private Vector2 direction;
    private RaycastHit2D raycastHit;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ToggleLine(false);
    }

    public void DrawToMouse(Vector2 from, LayerMask ignoreMask)
    {
        lineRenderer.SetPosition(0, from);
        direction = (currentMouseWorldPosition - from).normalized;
        for (int i = 1; i < positionCount; i++)
        {
            raycastHit = Physics2D.Raycast(from, direction, float.MaxValue, ~ignoreMask);
            if (raycastHit.collider != null)
            {
                lineRenderer.SetPosition(i, raycastHit.point);
                from = raycastHit.point + raycastHit.normal * 0.01f;
                direction = Vector2.Reflect(direction, raycastHit.normal);
            }
   
        }
    }

    public void ToggleLine(bool value)
    {
        lineRenderer.enabled = value;
    }
}
