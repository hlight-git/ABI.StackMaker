using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player target;
    [SerializeField] private Vector3 playingModeOffset;
    [SerializeField] private Vector3 cheeringModeOffset;
    [SerializeField] private float speed;

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {        
        if (!target.IsCheering)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + playingModeOffset, Time.fixedDeltaTime * speed);
        } else
        {
            Quaternion rotTarget = Quaternion.LookRotation(target.transform.position - this.transform.position);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, Time.fixedDeltaTime * speed * 2);
            transform.position = Vector3.Lerp(transform.position, target.transform.position + cheeringModeOffset, Time.fixedDeltaTime * 2);
        }
    }
}
