using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
   public static BallController instance;

   [SerializeField] private LineRenderer lineRenderObject;
   //[SerializeField] private GameObject areaAffector;
   [SerializeField] private float maxForce, forceModifier;
   //[SerializeField] private LayerMask rayLayer;

   private float force;
   private Rigidbody rdBody;

   private Vector3 startPos, endPos;
   private bool canShoot = false;
   private Vector3 direction;

   private void Awake() 
   {
    if(instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(gameObject);
    }
    
    rdBody = GetComponent<Rigidbody>();

   }
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !canShoot)
        {
            startPos = ClickedPoint();
            lineRenderObject.gameObject.SetActive(true);
            lineRenderObject.SetPosition(0, -lineRenderObject.transform.localPosition);
            
        }

        if(Input.GetMouseButton(0))
        {
            endPos = ClickedPoint();
            endPos.y = lineRenderObject.transform.position.y;
            force = Mathf.Clamp(Vector3.Distance(endPos, startPos) * forceModifier, 0, maxForce);
            lineRenderObject.SetPosition(1, -transform.InverseTransformPoint(endPos));
        }

        if(Input.GetMouseButtonUp(0))
        {
            canShoot = true;
            lineRenderObject.gameObject.SetActive(false);
        }
    }
    private void FixedUpdate() 
    {
        if(canShoot)
        {
            canShoot = false;
            direction = startPos - endPos;
            rdBody.AddForce(direction * force, ForceMode.Impulse);
            force = 0;
            startPos = endPos = Vector3.zero;
        }
    }


    Vector3 ClickedPoint()
    {
        Vector3 position = endPos;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            position = hit.point;
        }
        return position;
    }

}
