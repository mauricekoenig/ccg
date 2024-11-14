using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    public float zDepth;
    private LineRenderer lineRenderer;
    private bool init;
    private Camera cam;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        cam = Camera.main;
    }

    public void SetOrigin (Vector3 origin) {
        origin.z = -1f;
        lineRenderer.SetPosition(0, origin);
        init = true;
    }

    public void SetDestination (Vector3 destination) {
        destination.z = -1f;
        lineRenderer.SetPosition(1, destination);
    }

    public void Reset() {
        this.lineRenderer.SetPosition(0, Vector3.zero);
        this.lineRenderer.SetPosition(1, Vector3.zero);
    }
    private void Update() {
        if (!init) return;
        SetDestination(GetMouseWorldPosition(1));
    }

    private Vector3 GetMouseWorldPosition (float z) {

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = zDepth;
        return mouseWorld;
    }
}
