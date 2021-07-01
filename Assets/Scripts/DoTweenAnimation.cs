using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenAnimation : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetPos = Vector3.zero;
    [SerializeField][Range(0,1)]
    private float _MoveDuration;
    [SerializeField]
    private Ease _moveEase = Ease.Linear;
                                                
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CubeJumpAnimation()
    {
        if(_targetPos ==Vector3.zero)
        {
            _targetPos = transform.position;
            transform.DOMove(_targetPos, _MoveDuration);
        }
    }
   
}
