using UnityEngine;
using System.Collections.Generic;

public class EnemyRouteObject : MonoBehaviour
{
    public List<Transform> RoutePoints { get; private set; }

    private void Awake()
    {
        RoutePoints = new List<Transform>();

        foreach (Transform child in transform)
        {
            RoutePoints.Add(child);
        }
    }
}
