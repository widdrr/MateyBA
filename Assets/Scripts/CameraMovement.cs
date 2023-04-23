using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.1F;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    //Two Vector2 for the maximum area/ minimum area that the camera can go to.
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
	if(transform.position != target.position){
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            //We use clamp to limit the position of the camera between given coordinates.
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            //We use lerp to get any point between positions.
	}
    }
}
