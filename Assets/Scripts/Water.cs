using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class Water : MonoBehaviour
{
   
    [Range(0, 1)] public float ObjectPosition;
    [SerializeField] private SplineFollower[] m_Follower;
    public float PathLength;
   


    [Button]
    void SetRefs()
    {

        m_Follower = GetComponentsInChildren<SplineFollower>();
    }

    [Button]
    void PlaceWater()
    {




        GameObject localcube = Instantiate(GameManager.Instance.Water) as GameObject;
        localcube.transform.parent = transform;
        localcube.transform.localEulerAngles = Vector3.zero;
        localcube.transform.localPosition = Vector3.zero;
        //  localcube.transform.position = transform.position;
        localcube.transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), 0, 0);




    }
    void Start()
    {
        SetObjectsLocation();
        PlaceWater();
      

    }
    private void Awake()
    {
        PathLength =m_Follower[0].CalculateLength(0, 1) -20f;
    }


    void Update()
    {

    }

    private void SetObjectsLocation()
    {
        for (int i = 0; i <= m_Follower.Length - 1; i++)
        {

            m_Follower[i].SetPercent(ObjectPosition);
        }


    }
}
