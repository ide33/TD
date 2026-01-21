using UnityEngine;
using System.Collections.Generic;

public class EnemyRouteObject : MonoBehaviour
{
    [SerializeField] private Transform[] routePoints;
    public Transform[] RoutePoints => routePoints;

    // private void Awake()
    // {
    //     RoutePoints = new List<Transform>();

    //     foreach (Transform child in transform)
    //     {
    //         RoutePoints.Add(child);
            
    //     }
    // }
}
