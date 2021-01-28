using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Waypoint : MonoBehaviour
{
    bool selected = false;

    [SerializeField] public Waypoint next = null;

    //[SerializeField] GameObject prefab = null;
    
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
        
    }

    public void NewWayPoint()
    {
        print("NOT DONE YET");
        //GameObject wp = Instantiate(prefab, transform.position + transform.forward, Quaternion.identity, transform.parent);
        //next = wp;
    }

}

[CustomEditor(typeof(Waypoint))]
public class SomeScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Waypoint wp = (Waypoint)target;
        if (GUILayout.Button("Build Object"))
        {
            wp.NewWayPoint();
        }
    }
}
