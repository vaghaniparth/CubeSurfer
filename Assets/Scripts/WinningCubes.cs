using Dreamteck.Splines;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class WinningCubes : MonoBehaviour
{
    
    [SerializeField] private SplineFollower m_Follower;
    public float PathLength;

    [Button]
    void SetRefs()
    {

        m_Follower = GetComponentInChildren<SplineFollower>();
    }
  
    void Start()
    {
       
        SetObjectsLocation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetObjectsLocation()
    {

      PathLength=  m_Follower.CalculateLength(0, 1);
        m_Follower.SetDistance(PathLength-97.05f);

    }
}
