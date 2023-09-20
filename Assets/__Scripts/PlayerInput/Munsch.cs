using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munsch : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    Vector3 BaseScale;
    [SerializeField] float scaleSpeed = 0.2f;
    [SerializeField] float downScaleSpeed = 0.1f;
    [SerializeField] float scaleAmount = 1.1f;


    private List<GameObject> bodyParts;

    
    private void Start()
    {
        bodyParts = GetComponent<SnekBody>().bodyParts;
        BaseScale = bodyParts[0].transform.localScale;
    }

    
    private void Update()
    {
        //if 1 pressed munsch left to right
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            EatableMunsch(0);
        }
        //if 2 pressed munsch right to left
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EatableMunsch(1);
        }
        
    }

    
    public void EatableMunsch(byte dir = 0)
    {
        bodyParts = GetComponent<SnekBody>().bodyParts;

        if(dir == 0)
        {
            StartCoroutine(MunschLeftRight());
        }
        else if(dir == 1)
        {
            StartCoroutine(MunschRightLeft());
        }
        
    }


    IEnumerator MunschLeftRight()
    {
        //Foreach body parts to base scale * scaleAmount with scaleSpeed  scale down to base scale then next body part
        foreach (GameObject bodyPart in bodyParts)
        {
            LeanTween.scale(bodyPart, BaseScale * scaleAmount, scaleSpeed);
            while (bodyPart.transform.localScale.x < BaseScale.x * scaleAmount) yield return null;
            
            //scale down to base scale
            LeanTween.scale(bodyPart, BaseScale, downScaleSpeed);
            while (bodyPart.transform.localScale.x > BaseScale.x) yield return null;           
        }

        
    }

    IEnumerator MunschRightLeft()
    {
        List<GameObject> LeftbodyParts = Reverse(bodyParts);
        //Foreach body parts to base scale * scaleAmount with scaleSpeed  scale down to base scale then next body part
        foreach (GameObject bodyPart in LeftbodyParts)
        {
            LeanTween.scale(bodyPart, BaseScale * scaleAmount, scaleSpeed);
            while (bodyPart.transform.localScale.x < BaseScale.x * scaleAmount) yield return null;

            //scale down to base scale
            LeanTween.scale(bodyPart, BaseScale, downScaleSpeed);            
            while (bodyPart.transform.localScale.x > BaseScale.x) yield return null;
        }

    }

    private List<GameObject> Reverse(List<GameObject> list)
    {
        List<GameObject> reversedList = new List<GameObject>();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            reversedList.Add(list[i]);
        }
        return reversedList;
    }




    





}