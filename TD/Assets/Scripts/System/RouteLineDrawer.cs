using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RouteLineDrawer : MonoBehaviour
{
    [SerializeField] private EnemyRouteObject routeObject;
    [SerializeField] private Color lineColor = Color.red;
    [SerializeField] private float lineWidth = 0.1f;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawRoute();
    }

    void DrawRoute()
    {
        Transform[] points = routeObject.RoutePoints;

        lineRenderer.positionCount = points.Length;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
}