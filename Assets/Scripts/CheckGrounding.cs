using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckGrounding : MonoBehaviour
{
    public bool isGrounded;

    [SerializeField] LayerMask collisionLayers;
    [SerializeField] TextMeshProUGUI groundedDebugText;
    [SerializeField] List<Vector3> castsInitPos = new List<Vector3>();
    [SerializeField] float raySize;

    bool internalGroundCheck;

    void Update()
    {
        internalGroundCheck = false;
        for (int i = 0; i < castsInitPos.Count; i++)
        {
            Vector3 Origin = transform.TransformDirection(castsInitPos[i]) + transform.position;
            if (Physics.Raycast(transform.position, -Vector3.up, raySize, collisionLayers))
                internalGroundCheck = true;
            Debug.DrawRay(Origin, -Vector3.up * raySize, Color.yellow);
        }
        isGrounded = internalGroundCheck;
        UpdateDebugText();
    }

    private void UpdateDebugText()
    {
        if (isGrounded)
        {
            groundedDebugText.text = "Grounded";
            groundedDebugText.color = Color.green;
        }
        else
        {
            groundedDebugText.text = "UnGrounded";
            groundedDebugText.color = Color.yellow;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < castsInitPos.Count; i++)
        {
            Vector3 origin = transform.TransformDirection(castsInitPos[i]) + transform.position;
            Gizmos.DrawRay(origin, -Vector3.up * raySize);
        }
    }
}
