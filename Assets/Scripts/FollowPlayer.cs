using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;

public class FollowPlayer : MonoBehaviour
{
    public static FollowPlayer Instance = null;
    public bool IsWin;
    // public SplineProjector splineProjector;
    public SplineFollower thissplineFollower;
    public SplineFollower PlayerSplineFollower;
    public Transform playerTransform;
    public Transform CameraRootTransform;

    // Start is called before the first frame update
    
    void Awake()
    {

      
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        thissplineFollower = GetComponent<SplineFollower>();
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()

    {
        transform.position = playerTransform.position;
        thissplineFollower.followSpeed = GameManager.Instance.PlayerSpeed;
        transform.LookAt(PlayerSplineFollower.result.position);
       // CameraRootTransform.LookAt(playerTransform);


    }
    [Button]
    void Rotate()
    {   if (IsWin == true)
        {
            
        }
       
    }
    public void MoveCameraUp()
    {
        CameraRootTransform.position = new Vector3(transform.position.x-0.02f, transform.position.y + 1, transform.position.z);
    }


}
