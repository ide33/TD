using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class RouteLineDrawer : MonoBehaviour
{
    [SerializeField] private EnemyRouteObject routeObject;
    [SerializeField] private Color lineColor = Color.red;
    [SerializeField] private float lineWidth = 0.1f;

    private LineRenderer lineRenderer;
    private Coroutine hideCoroutine;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void ShowRoute(float displayTime)
    {
        DrawRoute();
        lineRenderer.enabled = true;

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideAfterTime(displayTime));
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

    IEnumerator HideAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        lineRenderer.enabled = false;
    }
}