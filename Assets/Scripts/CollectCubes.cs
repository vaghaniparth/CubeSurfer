using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;

public class CollectCubes : MonoBehaviour
{
  [SerializeField]  private SplineFollower[] m_Follower;
    [Range(0, 1)] public float ObjectPosition;
    
    
    [Button]
    void SetRefs()
    {
        
        m_Follower = GetComponentsInChildren<SplineFollower>();
    }
    [Button]
    void PlaceCube()
    {
      GameObject localcube =  Instantiate(GameManager.Instance.CollectCube) as GameObject;
        localcube.transform.parent = transform;
        localcube.transform.localEulerAngles =Vector3.zero;
        localcube.transform.localPosition =Vector3.zero;
        //  localcube.transform.position = transform.position;
        localcube.transform.localPosition = new Vector3 (Random.Range(-2,2),0.5f,0);
        

    }
    void Start()
    {
        SetObjectsLocation();
        PlaceCube();

    }

   
    void Update()
    {
       
    }
   
    private void SetObjectsLocation()
    {
        for (int i = 0; i <= m_Follower.Length-1; i++)
        {
         
            m_Follower[i].SetPercent(ObjectPosition);
        }
       
        
    }
   
}
