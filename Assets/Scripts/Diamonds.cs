using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class Diamonds : MonoBehaviour
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
    void PlaceDiamonds()
    {

        GameObject localcube = Instantiate(GameManager.Instance.Diamond) as GameObject;
        localcube.transform.parent = transform;
        localcube.transform.localEulerAngles = Vector3.zero;
        localcube.transform.localPosition = Vector3.zero;
        //  localcube.transform.position = transform.position;
        localcube.transform.localPosition = new Vector3(Random.Range(-2, 2), 1f, 0);


        /*GameObject localcube = Instantiate(GameManager.Instance.Diamond) as GameObject;
        localcube.transform.parent = transform;
        localcube.transform.localEulerAngles = Vector3.zero;
        localcube.transform.localPosition = Vector3.zero;
        //  localcube.transform.position = transform.position;
        localcube.transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, Random.Range(50, PathLength));*/


        //  localcube.transform.localPosition = new Vector3(Random.Range(-2f,2f), 0.5f, Random.Range(50, PathLength));




    }
    void Start()
    {
        SetObjectsLocation();
        PlaceDiamonds();


    }
    private void Awake()
    {
       
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
