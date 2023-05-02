using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public static BallController instance;

    [SerializeField]
    private LineRenderer lineRenderObject;

    [SerializeField]
    private float maxForce,forceModifier;

    [SerializeField]
    private float stopVelocity;

    private float force;
    private Rigidbody rdBody;
    public float LineRendererMaxDist;

    private Vector3 startPos,endPos;
    private bool canShoot = false;
    private Vector3 direction;

    private bool isIdle;
    private bool isAiming;

    private void Awake()
    {
        isAiming = false;
        lineRenderObject.enabled = false;
        isIdle = true;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rdBody = GetComponent<Rigidbody>();
    }

    void Start() { }

    void Update()
    {
        if (rdBody.velocity.magnitude < stopVelocity)
        {
            Stop();
        }

        if (Input.GetMouseButtonDown(0) && !canShoot && isIdle)
        {   
            lineRenderObject.enabled = true;
            startPos = ClickedPoint();
            lineRenderObject.gameObject.SetActive(true);
            lineRenderObject.SetPosition(0, -lineRenderObject.transform.localPosition);
        }

        if (Input.GetMouseButton(0) && !canShoot && isIdle)
        {
            endPos = ClickedPoint();
            endPos.y = lineRenderObject.transform.position.y;
            float distance = Vector3.Distance(endPos, startPos);
            if (distance > LineRendererMaxDist) // line renderer sınırlandırma
            {
                endPos = startPos + (endPos - startPos).normalized * LineRendererMaxDist;
                distance = LineRendererMaxDist;
            }
            force = Mathf.Clamp(distance * forceModifier, 0, maxForce);
            lineRenderObject.SetPosition(1, -transform.InverseTransformPoint(endPos));
        }

        if (Input.GetMouseButtonUp(0) && !canShoot && isIdle)
        {
            canShoot = true;
            lineRenderObject.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (canShoot && isIdle)
        {
            canShoot = false;
            direction = startPos - endPos;
            rdBody.AddForce(direction * force, ForceMode.Impulse);
            force = 0;
            startPos = endPos = Vector3.zero;
            isIdle = false;
        }
    }

    Vector3 ClickedPoint()
    {
        Vector3 position = endPos;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = hit.point;
        }
        return position;
    }

    public void Stop()
    {
        rdBody.velocity = Vector3.zero;
        rdBody.angularVelocity = Vector3.zero;
        isIdle = true;
    }
}
