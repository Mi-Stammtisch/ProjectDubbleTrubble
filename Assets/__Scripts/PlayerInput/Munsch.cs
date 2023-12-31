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

    
    IEnumerator Start()
    {

        
        bodyParts = GetComponent<SnekBody>().bodyParts;
        while (bodyParts.Count == 0) yield return null;
        
        
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

    
    public void EatableMunsch(byte dir, GameObject projectile = null)
    {
        bodyParts = GetComponent<SnekBody>().bodyParts;
        //Debug.Log("bodyParts.Count: " + bodyParts.Count);

        if(dir == 0)
        {
            StartCoroutine(MunschLeftRight(projectile));
        }
        else if(dir == 1)
        {
            StartCoroutine(MunschRightLeft(projectile));
        }
        
    }


    IEnumerator MunschLeftRight(GameObject gameObject)
    {
        bodyParts = GetComponent<SnekBody>().bodyParts;
        Debug.Log("bodyParts.Count: " + bodyParts.Count);
        
        //Foreach body parts to base scale * scaleAmount with scaleSpeed  scale down to base scale then next body part
        foreach (GameObject bodyPart in bodyParts)
        {
            GameObject bodyPartClone = Instantiate(bodyPart, bodyPart.transform.position, bodyPart.transform.rotation);
            bodyPartClone.transform.SetParent(bodyPart.transform.parent);
            bodyPartClone.transform.localScale = bodyPart.transform.localScale;
            bodyPartClone.GetComponent<Collider>().enabled = false;

            LeanTween.scale(bodyPartClone, BaseScale * scaleAmount, scaleSpeed);
            while (bodyPartClone.transform.localScale.x < BaseScale.x * scaleAmount) yield return null;           
           
            
            LeanTween.scale(bodyPartClone, BaseScale, downScaleSpeed);
            while (bodyPartClone.transform.localScale.x > BaseScale.x) yield return null;  

            LeanTween.cancel(bodyPartClone);
            Destroy(bodyPartClone);       
        }

        if(gameObject != null)
        {
            player2.GetComponent<PlayerSpitter>().SpawnProjectile(gameObject);
        }
        

        
    }

    IEnumerator MunschRightLeft(GameObject gameObject)
    {
        bodyParts = GetComponent<SnekBody>().bodyParts;
        //Debug.Log("bodyParts.Count: " + bodyParts.Count);
        List<GameObject> LeftbodyParts = Reverse(bodyParts);
        //Foreach body parts to base scale * scaleAmount with scaleSpeed  scale down to base scale then next body part
        foreach (GameObject bodyPart in LeftbodyParts)
        {
            GameObject bodyPartClone = Instantiate(bodyPart, bodyPart.transform.position, bodyPart.transform.rotation);
            bodyPartClone.transform.SetParent(bodyPart.transform.parent);
            bodyPartClone.transform.localScale = bodyPart.transform.localScale;
            bodyPartClone.GetComponent<Collider>().enabled = false;

            LeanTween.scale(bodyPartClone, BaseScale * scaleAmount, scaleSpeed);
            while (bodyPartClone.transform.localScale.x < BaseScale.x * scaleAmount) yield return null;

            //scale down to base scale
            LeanTween.scale(bodyPartClone, BaseScale, downScaleSpeed);            
            while (bodyPartClone.transform.localScale.x > BaseScale.x) yield return null;

            LeanTween.cancel(bodyPartClone);
            Destroy(bodyPartClone);
        }

        if (gameObject != null)
        {
            player1.GetComponent<PlayerSpitter>().SpawnProjectile(gameObject);
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
