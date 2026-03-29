using UnityEngine;
using System.Collections.Generic;

public class EnemyRouteObject : MonoBehaviour
{
    [SerializeField] private Transform[] routePoints;
    public Transform[] RoutePoints => routePoints;
}
