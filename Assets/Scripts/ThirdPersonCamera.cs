using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Transform subject;
    [SerializeField] Vector3 defaultOffset = new Vector3(0, 2, -10);
    [SerializeField] float interpolationFactor = 0.3f;

    Vector3 velocity = Vector3.one;
    
    void LateUpdate()
    {
        //Interpolation Linéraire, effet "smooth" / élastique
        Vector3 nextPosition = subject.position + (subject.rotation * defaultOffset);
        transform.position = Vector3.SmoothDamp(transform.position, nextPosition, ref velocity, interpolationFactor);

        transform.LookAt(subject, subject.up);
    }
}
