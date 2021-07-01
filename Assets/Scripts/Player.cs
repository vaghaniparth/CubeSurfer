using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using DG.Tweening;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField, ReadOnly] private SplineFollower m_Follower;
    Tween m_JumpTween,m_ScaleTween;
    [SerializeField] Transform PlayerVisual;
    [SerializeField] Transform CollectedCubes;
    [SerializeField] Transform RemovedCubes;
    // [SerializeField] GameObject RemovedCube;
    [SerializeField] Ease MoveEase = Ease.OutBack;
    public LayerMask layerMask;
    [SerializeField] GameObject WinUI;
    public Text ProgressText;
    public Ray ray;
    public Transform RootVisual;
    public bool IsDown;
    public Vector3 OldPosition;    
    public Vector3 Axis;
    public float Sensitivity;
    public float TouchForwardSens;
    [SerializeField]
    List<GameObject> PlayerChildList = new List<GameObject>();
    //public BoxCollider cube;
    public bool CollidedBefore;
    public bool RemovedBefore;
    private  float Timer;
    public float y;
    public int ChildCount;
    


    [Button]
    float GetProgress()
    {
       // m_Follower.
        return 0;
    }


    [Button]
    void SetRefs()
    {   
        //cube = GetComponentInChildren<BoxCollider>();
        m_Follower = GetComponent<SplineFollower>();
    }
    private void Start()
    {   
        SetPlayerPosition();
       
       
    }


    void Update()
    { 
        if (GameManager.Instance.PlayerSpeed > 0)
        {
                if (Input.GetMouseButtonDown(0))
                {
                     IsDown = true;
                }

               if (Input.GetMouseButton(0))
               {
                 if (OldPosition != Vector3.zero)
                 {
                     Axis.x = Input.mousePosition.x - OldPosition.x;
                     Axis.x *= Sensitivity;
                 }

                 OldPosition = Input.mousePosition;
               }
                if (Input.GetMouseButtonUp(0))
                {
                    IsDown = false;
                    OldPosition = Vector3.zero;
                }
        }
            RootVisual.localPosition += Axis;
            Vector3 tmp = RootVisual.localPosition;
            tmp.x = Mathf.Clamp(tmp.x, -2, 2);
            RootVisual.localPosition = tmp;
            m_Follower.followSpeed = GameManager.Instance.PlayerSpeed;
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        
        {
            AddCube();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))

        {
            RemoveCube("Opps Try Again");
        }if (Input.GetKeyDown(KeyCode.Alpha3))

        {
            SetPlayerPosition();
        }if (Input.GetKeyDown(KeyCode.Alpha4))

        {
            GameManager.Instance.PlayerSpeed += 10;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.Instance.PlayerSpeed == 10)
            {
                GameManager.Instance.PlayerSpeed = 0;
            }
            else
            {
                GameManager.Instance.PlayerSpeed = 10;
            }
        } 

    }


    private void OnTriggerEnter(Collider col)
    {  
        if (col.tag == "Objects")
        {
          //  Debug.Log(col.gameObject.name);
            Destroy(col.gameObject);
            AddCube();
            //  Debug.Log(col.gameObject.name);
        }
        if (col.tag == "WallCube"){/* Debug.Log(col.gameObject.name);*/}
        if (col.tag =="1Cube"){ y = 0; RemoveLoop(1);} 
        if (col.tag =="2Cube"){ y = 0; RemoveLoop(2);}
        if (col.tag =="3Cube"){ y = 0; RemoveLoop(3);} 
        if (col.tag =="Water"){ Timer=0;}
        if (col.tag =="SpeedUp"){ GameManager.Instance.PlayerSpeed += 10; StartCoroutine( NormalGameSpeed()); }
        if (col.tag == "Diamond")
        {
            GameManager.Instance.CurrentLevelDiamond = 1;
            Destroy(col.gameObject);
        }
        if (col.tag=="WinStair"){DropCube(1,col.gameObject.name); FollowPlayer.Instance.MoveCameraUp(); }
        if (col.tag=="WinStair20"){DropCube(2,col.gameObject.name); FollowPlayer.Instance.MoveCameraUp(); }
        if (col.tag=="Finish")
        {
            GameManager.Instance.PlayerSpeed = 10;
            DOVirtual.Float(10,0,10.5f,i_NewSpeed=>
            {
                GameManager.Instance.PlayerSpeed = i_NewSpeed;
            });

            
        }



    }
    private void OnTriggerStay(Collider other)
    {
        if (RemovedBefore) return;
        RemovedBefore = true;
        DOVirtual.DelayedCall(0.25f, () =>
        {
            RemovedBefore = false;
        });
        if (other.tag == "Water")
        {
            RemoveCube("Opps Try Again");
        }
       
    }

    IEnumerator NormalGameSpeed()
    {
        yield return new WaitForSeconds(4);
        GameManager.Instance.PlayerSpeed = 10;
    }
    public void RemoveLoop(int Times)
    {  
        if (CollidedBefore) return;
        CollidedBefore = true;
        DOVirtual.DelayedCall(1, ()=>
        {
            CollidedBefore = false;
        });
        if (PlayerChildList.Count < Times)
        {
            GameManager.Instance.PlayerSpeed = 0;
            ProgressTextFunction("Opps Try Again");
            WinUISetActive();
            return;
        }
        for (int i = 0; i < Times; i++)
        {           
            DropCube(0,"Opps Try Again");            
        }
    }

    [Button]
    public void AddCube()
    {       
        GameObject localNewCube = Instantiate(GameManager.Instance.CollectCube);
        localNewCube.GetComponent<Collider>().enabled = false;
        Vector3 localTargetPosition = Vector3.zero;
        localTargetPosition.y = 0.5f;
        for (int i = 0; i < CollectedCubes.childCount; i++)
        {
            localTargetPosition.y += 1;
        }

        localNewCube.transform.parent =CollectedCubes ;
        localNewCube.transform.eulerAngles = transform.eulerAngles;
        //localNewCube.transform.localPosition = localTargetPosition;
      //  Debug.Log(localTargetPosition);
        m_JumpTween?.Kill(true);
        m_JumpTween = localNewCube.transform.DOLocalMove(localTargetPosition, 0.2f).SetEase(MoveEase);
        m_ScaleTween?.Kill(true);
        localNewCube.transform.localScale = Vector3.zero;
        m_ScaleTween= localNewCube.transform.DOScale(1, 0.5f);
        localNewCube.name = localNewCube.transform.localPosition.y.ToString();
        PlayerChildList.Add(localNewCube);
       
        UpdatePlayerPos(0);
    }

    [Button]
    public void RemoveCube(string Progress)        
    {
       
        if (CollectedCubes.childCount == 0)
        {
            GameManager.Instance.PlayerSpeed = 0;
            ProgressTextFunction("Opps Try Again");
            WinUISetActive();
            return;
        }
        Destroy(PlayerChildList[PlayerChildList.Count - 1]);
        PlayerChildList.RemoveAt(PlayerChildList.Count - 1);
        RemovedBefore = true;
        UpdatePlayerPos(0);          
    }


    [Button]
    public void DropCube(int ForStair,string Progress)
        
    {
        
        if (PlayerChildList.Count == 0)
        {
            GameManager.Instance.PlayerSpeed = 0;
            ProgressTextFunction(Progress);
            WinUISetActive();
            return;
        }
           
       
        PlayerChildList[0].transform.parent = RemovedCubes;
        
        ChildCount++;
        int CurrentChild = ChildCount - 1;
        DropedCubePosition(CurrentChild,y);
        PlayerChildList.RemoveAt(0);
        UpdatePlayerPos(ForStair);
        y++;
    }
    public void UpdatePlayerPos(int ForStair)
    {   
        float LocalCubeHight =GetGroundHight();     
        for (int i = 0; i < PlayerChildList.Count; i++)
        {   
            Vector3 localV3 = new Vector3(0,0.5f+LocalCubeHight+i+ForStair, 0);
            PlayerChildList[i].transform.localPosition = localV3;
            
        }
        Vector3 PlayerPos = RootVisual.localPosition;
        PlayerPos.y = PlayerChildList.Count +LocalCubeHight+ForStair;
        PlayerPos.x = 0;
        
        m_JumpTween?.Kill(true);
        m_JumpTween= PlayerVisual.DOLocalMove(PlayerPos,1).SetEase(MoveEase);

       
         // PlayerJumpAnimation(PlayerVisual.position);

    }
    [Button]
    float GetGroundHight()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(PlayerVisual.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
          //  Debug.DrawRay(PlayerVisual.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow,10);
          //  Debug.Log("Did Hit");
         //   Debug.Log(hit.point.y);
            return hit.point.y;
        }
        else
        {
           // Debug.DrawRay(PlayerVisual.position, transform.TransformDirection(Vector3.down) * 1000, Color.white,10);
           // Debug.Log("Did not Hit");
            
        }
        return 0;
    }

    

    public void DropedCubePosition(int CurrentChild,float YPos)
    {
       
        RemovedCubes.GetChild(CurrentChild).localPosition = new Vector3(RemovedCubes.GetChild(CurrentChild).localPosition.x, YPos, RemovedCubes.GetChild(CurrentChild).localPosition.z);
       // Destroy(RemovedCubes.GetChild(CurrentChild).gameObject , 3);
    }



    [Button]
    void SetPlayerPosition()
    {       
        m_Follower.SetDistance(14);
    }
   /* public void SlideValuePlus()
    {
        GameManager.Instance.SliderPlus();
    }*/
   void WinUISetActive()
    {
        WinUI.SetActive(true);
    }
    void ProgressTextFunction(string Progress)
    {
        ProgressText.text = Progress;
    } 
    



}