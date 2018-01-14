using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    [Header("Look target")]
    [SerializeField]
    protected Transform target;

    [Header("Smooth parameters")]
    [SerializeField]
    protected bool smooth = true;
    [SerializeField] [Range(0, 1)] protected float smoothStep = 0.125f;

    [Header("Offset form target")]
    [SerializeField]
    protected Vector3 offset = new Vector3(0, 5, -5);

    protected Vector3 velocity = Vector3.one;

    protected void SmoothFollow()
    {
        Vector3 desiredPosition = target.position + (target.rotation * offset);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothStep);

        transform.LookAt(target, target.up);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        target.transform.Translate(Vector3.forward * 50 * Time.deltaTime);
	}


    private void LateUpdate()
    {
        SmoothFollow();
    }
}
