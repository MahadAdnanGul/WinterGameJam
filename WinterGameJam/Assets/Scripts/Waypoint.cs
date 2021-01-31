using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Waypoint : MonoBehaviour
{
    bool selected = false;

    [SerializeField] public Waypoint next = null;
    [SerializeField] public Waypoint shortcut = null;

    [SerializeField] Waypoint prefab = null;


    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.DrawRay(transform.position, transform.forward);
        if (next)
        {
            Debug.DrawLine(transform.position, next.transform.position, Color.blue);
        }
        if (shortcut)
        {
            Debug.DrawLine(transform.position, shortcut.transform.position, Color.red);
        }

    }

    public void NewWayPoint()
    {
        Waypoint wp = Instantiate(prefab, transform.position + transform.forward, Quaternion.identity, transform.parent);
        wp.name = gameObject.name;
        next = wp;
        Selection.SetActiveObjectWithContext(wp, this);
    }

}

[CustomEditor(typeof(Waypoint))]
public class SomeScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Waypoint wp = (Waypoint)target;
        if (GUILayout.Button("Build Waypoint"))
        {
            wp.NewWayPoint();
        }
    }
}
