using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;

public class Wall : MonoBehaviour
{
    [SerializeField] private SplineFollower[] m_Follower;
    [Range(0, 1)] public float ObjectPosition;


    [Button]
    void SetRefs()
    {

        m_Follower = GetComponentsInChildren<SplineFollower>();
    }
    [Button]
    void PlaceWall()
    {
        float XPos = -2f;
        for (int i = 0; i < 5; i++)
        {
            

            GameObject localcube = Instantiate(GameManager.Instance.CombinedWall[Random.Range(0, GameManager.Instance.CombinedWall.Length)]) as GameObject;
            localcube.transform.parent = transform;
            localcube.transform.localEulerAngles = Vector3.zero;
            localcube.transform.localPosition = Vector3.zero;
            //  localcube.transform.position = transform.position;
            localcube.transform.localPosition = new Vector3(XPos, 0.5f, 0);
            XPos += 1;
        }


    }
    void Start()
    {
        SetObjectsLocation();
        PlaceWall();

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
