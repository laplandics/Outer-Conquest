using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public static class RayCaster
{
    public static bool IsMouseOverUi(out List<RaycastResult> uiHits)
    {
        var raycastResults = new List<RaycastResult>();
        var data = new PointerEventData(EventSystem.current) { position = Mouse.current.position.ReadValue() };
        EventSystem.current.RaycastAll(data, raycastResults);
        var resultsOverUi = raycastResults.Where(r => r.gameObject.layer == LayerMask.NameToLayer("UI")).ToList();
        uiHits = resultsOverUi;
        return resultsOverUi.Count > 0;
    }
    
    public static bool GetMouseHit(out RaycastHit hit)
    {
        hit = new RaycastHit();
        var cam = Camera.main;
        var mouse = Mouse.current.position.ReadValue();
        if (cam == null) return false;
        var ray = cam.ScreenPointToRay(mouse);
        return Physics.Raycast(ray, out hit);
    }

    public static bool GetMousePosition(out Vector3 worldPosition, out Vector2 screenPosition)
    {
        worldPosition = Vector3.zero;
        var cam = Camera.main;
        var mouse = Mouse.current.position.ReadValue();
        screenPosition = mouse;
        if (cam == null) return false;
        var ray = cam.ScreenPointToRay(mouse);
        var plane = new Plane(Vector3.up, Vector3.zero);
        if (!plane.Raycast(ray, out var distance)) return false;
        worldPosition = ray.GetPoint(distance);
        return true;
    }

    public static Vector3[] GetFrustumPoints(Vector2 startMouseScreenPos, Vector2 endMouseScreenPos)
    {
        const float nearDistance = 1f;
        const float farDistance = 100f;
        var cam = Camera.main;
        if (cam == null) return Array.Empty<Vector3>();
        var nearPlane = new Plane(cam.transform.forward, cam.transform.position + cam.transform.forward * nearDistance);
        var farPlane  = new Plane(cam.transform.forward, cam.transform.position + cam.transform.forward * farDistance);
        var minX = Mathf.Min(startMouseScreenPos.x, endMouseScreenPos.x);
        var maxX = Mathf.Max(startMouseScreenPos.x, endMouseScreenPos.x);
        var minY = Mathf.Min(startMouseScreenPos.y, endMouseScreenPos.y);
        var maxY = Mathf.Max(startMouseScreenPos.y, endMouseScreenPos.y);
        var screenCorners = new Vector2[] { new(minX, minY), new(maxX, minY), new(maxX, maxY), new(minX, maxY) };
        var nearPoints = new Vector3[4];
        var farPoints  = new Vector3[4];
        for (var i = 0; i < 4; i++)
        {
            var ray = cam.ScreenPointToRay(screenCorners[i]);
            if (nearPlane.Raycast(ray, out var nearEnter)) nearPoints[i] = ray.GetPoint(nearEnter);
            if (farPlane.Raycast(ray, out var farEnter)) farPoints[i] = ray.GetPoint(farEnter);
        }
        var points = new[] { nearPoints[0], nearPoints[1], nearPoints[2],
            nearPoints[3], farPoints[0], farPoints[1], farPoints[2], farPoints[3] };
        return points;
    }
}
