using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class FollowSpline : MonoBehaviour
{
    SplineTracer tracer;
    private void Awake()
    {
    tracer = GetComponent<SplineTracer>();
    }
    private void OnEnable()
    {
    tracer.onNode += OnNode; //onNode is called every time the tracer passes by a Node
    }
    private void OnDisable()
    {
    tracer.onNode -= OnNode;
    }

    private void OnNode (List<SplineTracer.NodeConnection> passed)
    {
        Debug.Log("Reached node " + passed[0].node.name + " connected at point " + 
        passed[0].point);
        Node.Connection[] connections = passed[0].node.GetConnections();
        
        if (connections.Length == 1) return;
        int newConnection = Random.Range(0, connections.Length);

        if (connections[newConnection].spline == tracer.spline && 
        connections[newConnection].pointIndex == passed[0].point)
        {
            newConnection++;
            if (newConnection >= connections.Length) newConnection = 0;
        }
        
        SwitchSpline(connections[newConnection]);
    }
 
    void SwitchSpline(Node.Connection to)
    {
        tracer.spline = to.spline;
        tracer.RebuildImmediate();
        double startpercent = tracer.ClipPercent(to.spline.GetPointPercent(to.pointIndex));
        tracer.SetPercent(startpercent);
    }
}
