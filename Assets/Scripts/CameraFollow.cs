using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    [SerializeField] private Transform followTarget;
    void Update()
    {
        var followDir = new Vector2
        (
            followTarget.transform.position.x - transform.position.x,
            followTarget.transform.position.y - transform.position.y
        );
        followDir.Normalize();

        var followDistance = Vector2.Distance(transform.position, followTarget.position);
        
        transform.Translate(followDir * Time.deltaTime * followDistance * followSpeed);
    }
}
