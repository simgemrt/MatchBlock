using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public List<CubeClass> cubeList = new List<CubeClass>(); 
    public CubeClass currentCube; 
    public Transform spawnPoint; 
    
    private Touch touch;
    private Vector3 down, up;
    private bool started;


    void Start()
    {
        currentCube = PickRandomCube();
    }

    void Update()
    {
        if(Input.touchCount>0) 
        {
            touch = Input.GetTouch(0); 
            if(touch.phase==TouchPhase.Began) 
            {
                started = true;
                down = touch.position;
                up = touch.position;
            }
        }
        if(started) 
        {
            if(touch.phase==TouchPhase.Moved)
            {
                down = touch.position; 

            }
            if(currentCube) 
            {
                currentCube.rb.velocity = CalculateDirection() * 8; 
            }
            if(touch.phase==TouchPhase.Ended) 
            {
                
                down = touch.position;
                started = false;
                if (!currentCube) return; 
                
                currentCube.rb.velocity = Vector3.zero; 
                currentCube.SendCube();
                currentCube = null; 
                StartCoroutine(SetCube());
            }
        }
    }
    private IEnumerator SetCube()
    {
        yield return new WaitForSeconds(1);
        currentCube = PickRandomCube();
    }
    private CubeClass PickRandomCube()
    {
        GameObject temp = Instantiate(cubeList[Random.Range(0, cubeList.Count)].gameObject, spawnPoint.position, Quaternion.identity); 
        return temp.GetComponent<CubeClass>();
    }

    private Vector3 CalculateDirection()
    {
        Vector3 temp = (down - up).normalized;
        temp.z = 0;
        temp.y = 0;
        return temp;
    }

}
