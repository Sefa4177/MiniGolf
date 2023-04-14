using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineForce : MonoBehaviour
{
   

    [SerializeField]private float shotPower;
    [SerializeField]private float stopVelocity;
    [SerializeField]private LineRenderer lineRenderer;

    private bool isIdle;
    private bool isAiming; 

    private Rigidbody rigidbody;


    private void Awake() 
    {
        rigidbody = GetComponent<Rigidbody>();

        isAiming = false;
        lineRenderer.enabled = false;
    }

    private void Update() 
    {
        if(rigidbody.velocity.magnitude < stopVelocity)
        {
            Stop();
        }

        ProcessAim();
           
    }

    private void OnMouseDown() 
    {
        if(isIdle)
        {
            isAiming = true;
        }
    }

    private void ProcessAim()
    {
      if(!isIdle || !isAiming)
        {
            return;
        } 
        
        Vector3? worldPoint = CastMouseClickRay();

        if(!worldPoint.HasValue)
        {
            return;
        }
        DrawLine(worldPoint.Value);
        

        if(Input.GetMouseButtonUp(0))
        {
            Shoot(worldPoint.Value);
        }

      
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        rigidbody.AddForce(direction * strength * shotPower);
        isIdle = false;
    }

    public void Stop()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        isIdle = true;
    }

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions = {
            transform.position,
            worldPoint};
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane); 
        Vector3 worldMouseFar =Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMouseNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;

        if(Physics.Raycast(worldMouseNear,worldMouseFar - worldMouseNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }

}
