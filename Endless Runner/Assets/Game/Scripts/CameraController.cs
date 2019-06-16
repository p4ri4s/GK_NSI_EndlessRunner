using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform followTarget;
    private Vector3 startOffset;
    private Vector3 moveVector;
    private float introAnimTime = 8.3f;

    void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - followTarget.position;
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad > introAnimTime)
        {
            moveVector = followTarget.position + startOffset;
            moveVector.x = 0;
            moveVector.y = transform.position.y;


            transform.position = moveVector;
        }
    }
}
