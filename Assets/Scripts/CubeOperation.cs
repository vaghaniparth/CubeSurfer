/*using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOperation : MonoBehaviour
{



    // List<GameObject> SpawnedCubes =  new List<GameObject>();
    [SerializeField] GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Objects")
        {
            Debug.Log(col.gameObject.name);
            Destroy(col.gameObject);
            CollectCube();
            //  Debug.Log(col.gameObject.name);
        }
        if (col.tag == "WallCube")
        {
            Debug.Log(col.gameObject.name);
            DestroyDownCube(col.gameObject);
        }

    }
    [Button]
    private void CollectCube()
    {
        GameObject localNewCube = Instantiate(GameManager.Instance.CollectCube);
        Vector3 localTargetPosition = Vector3.zero;
        localTargetPosition.y = 0.5f;
        for (int i = 0; i < Cubes.childCount; i++)
        {
            localTargetPosition.y += 1;

        }

        localNewCube.transform.parent = Cubes;
        localNewCube.transform.localPosition = localTargetPosition;
        localNewCube.name = localNewCube.transform.localPosition.y.ToString();

        Vector3 PlayerPos = RootVisual.position;
        PlayerPos.y = localTargetPosition.y + 0.5f;
        PlayerVisual.position = PlayerPos;


    }
    [Button]
    private void DestroyDownCube(GameObject target)
    {
        //  Debug.Log("Obstacles");

        for (int i = 0; i < SpawnedCubes.Count; i++)
        {
            SpawnedCubes[i].transform.parent = target.transform;
        }

    }

}
*/