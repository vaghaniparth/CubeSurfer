using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;

public class SpinWall : MonoBehaviour
{
    [SerializeField] private SplineFollower[] m_Follower;
    [Range(0, 1)] public float ObjectPosition;
    [SerializeField] float speed =100f;
    [SerializeField]private  Transform root;

    [Button]
    void SetRefs()
    {

        m_Follower = GetComponentsInChildren<SplineFollower>();
    }
    [Button]
    void PlaceWall()
    { float ZPos = 0.066f;
        float XPos = -0.0665f;
        for (int i = 0; i < 10; i++)
        { 
            if (i > 4) { ZPos = -0.66f; }
            if(i==5) { XPos = -0.0665f; }

            GameObject localcube = Instantiate(GameManager.Instance.CombinedWall[Random.Range(0, GameManager.Instance.CombinedWall.Length)]) as GameObject;
            localcube.transform.parent = root;          
            localcube.transform.localPosition = Vector3.zero;    
            localcube.transform.localPosition = new Vector3(XPos, 1.1f, ZPos);      
            XPos += 0.0665f;
        }

       


    }
    void Start()
    {
        SetObjectsLocation();
        PlaceWall();
       // m_Follower[0].motion.

    }


    void Update()
    { 
      root.Rotate(Vector3.up * Time.deltaTime * speed);
      
       
     // transform.localEulerAngles += new Vector3(0, 1, 0);
    }

    private void SetObjectsLocation()
    {
        for (int i = 0; i <= m_Follower.Length - 1; i++)
        {

            m_Follower[i].SetPercent(ObjectPosition);
        }


    }

}
