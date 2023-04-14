using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public GameObject Object;
    Vector3 OffSet;

    public Vector2 turn;
    public float sensivity;
    void Start()
    {
        OffSet = transform.position - Object.transform.position;
    }

        void Update()
    {
        transform.position = Object.transform.position + OffSet;
        turn.y += Input.GetAxis("Mouse Y") * sensivity;
        turn.x += Input.GetAxis("Mouse X") * sensivity;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x,0);

        
    }
}
