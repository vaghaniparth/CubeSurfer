using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    [SerializeField] public float PlayerSpeed = 8;
    public GameObject CollectCube;
    public GameObject[] CombinedWall;
    public GameObject Water;
    public GameObject Speedup;
    public GameObject Diamond;
    public Text DiamondText;
    public GameObject WinningCubes;
    public Text CollectedDiamonds;
    
    private static float TotalDiamonds ;
    private static float CollectDiamond ;
    public float diamondcount
    {
        get {return TotalDiamonds; }
        set { TotalDiamonds += value ;
            DiamondText.text = diamondcount.ToString();
           }
        
    } public float CurrentLevelDiamond
    {
        get {return CollectDiamond; }
        set { CollectDiamond += value ;
            CollectedDiamonds.text = CurrentLevelDiamond.ToString();
            diamondcount = 1;
        }
        
    }
  // [SerializeField] public Slider slider;
    void Awake()
    {
        TotalDiamonds = PlayerPrefs.GetFloat("DiamondCount", TotalDiamonds);
        DiamondText.text = diamondcount.ToString();
       

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
   
    void Start()
    {
        CurrentLevelDiamond = 0;
        CollectedDiamonds.text = CurrentLevelDiamond.ToString();
        WinningCubes.SetActive(true);
        //  slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* public void SliderPlus()
     {
         slider.value += 0.5f;
     }*/
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("DiamondCount", TotalDiamonds);
        PlayerPrefs.Save();
    }
    
   
}
