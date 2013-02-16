using UnityEngine;
using System.Collections;

public class StickyObject : MonoBehaviour
{
    void OnCollisionEnter(Collision c)
    {
        var joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = c.rigidbody;
    }
}