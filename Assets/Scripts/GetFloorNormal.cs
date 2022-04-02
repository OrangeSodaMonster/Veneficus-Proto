using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetFloorNormal : MonoBehaviour
{
    public Vector3 normalsAverage;
    public Vector3 normalsSum;

    [SerializeField] List<Vector2> castsInitPos = new List<Vector2>();
    [SerializeField] float yOffset;
    [SerializeField] float size;

    [SerializeField] Vector3 originDebugNormal;
    [SerializeField] bool showDebugRays;
    [SerializeField] TextMeshProUGUI normalDebugText;

    List<Vector3> normals = new List<Vector3>();

    private void Start()
    {
        foreach(Vector2 cast in castsInitPos)
        {
            normals.Add(Vector3.zero);
        }
    }

    void FixedUpdate()
    {       
        for(int i = 0; i < castsInitPos.Count; ++i)
        {
            RaycastHit hit;
            Vector3 Origin = transform.TransformDirection(new Vector3(castsInitPos[i].x, yOffset, castsInitPos[i].y)) + transform.position;
            if (Physics.Raycast(Origin, Vector3.down, out hit, size))
            normals[i] = hit.normal;

            if(showDebugRays) Debug.DrawRay(Origin, Vector3.down * size, Color.red);
        }

        normalsSum = Vector3.zero;
        foreach (Vector3 normal in normals)
        {
            normalsSum += normal;
        }

        normalsAverage = normalsSum/(float)normals.Count;        
        normalDebugText.text = $"Ground Normal :{normalsAverage}";
        //Debug.Log($"Ground Normal :{normalsAverage}");

        if (showDebugRays) Debug.DrawRay(originDebugNormal + transform.position, normalsAverage, Color.green);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < castsInitPos.Count; ++i)
        {
            Vector3 Origin = transform.TransformDirection(new Vector3(castsInitPos[i].x, yOffset, castsInitPos[i].y)) + transform.position;
            Gizmos.DrawRay(Origin, Vector3.down * size);
        }
    }
}
