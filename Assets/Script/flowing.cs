using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowing : MonoBehaviour {

    public float WaterLevel = 1f;
    public float FloatHeight = 2f;
    public float bounceDamp = 0.01f;
    public Vector3 Offset;
    private Rigidbody rb;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 upLift;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        actionPoint = transform.position + transform.TransformDirection(Offset);
        forceFactor = 1f - ((actionPoint.y - WaterLevel) / FloatHeight);

        if (forceFactor > 0f) {
            upLift = -Physics.gravity * (forceFactor - rb.velocity.y * bounceDamp);
            rb.AddForceAtPosition(upLift, actionPoint);
        }

	}
}
