using UnityEngine;
using UnityEngine.AI;

public static class NavMeshHelper
{
    public static bool IsOnNavMesh(Vector3 position, out NavMeshHit hit, float maxDistance = 0.8f) =>
        NavMesh.SamplePosition(position, out hit, maxDistance, NavMesh.AllAreas);

    public static Plane[] BuildPlanes(Vector3[] points)
    {
        var planes = new Plane[6];
        planes[0] = new Plane(points[0], points[1], points[2]);
        planes[1] = new Plane(points[6], points[5], points[4]);
        planes[2] = new Plane(points[0], points[3], points[7]);
        planes[3] = new Plane(points[2], points[1], points[5]);
        planes[4] = new Plane(points[1], points[0], points[4]);
        planes[5] = new Plane(points[3], points[2], points[6]);
        return planes;
    }
    
}