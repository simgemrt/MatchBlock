using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeClass : MonoBehaviour
{
    public int value; 
    public GameObject nextCube; 
    public Rigidbody rb;
    public int ID; 

    private void Awake()
    {
        ID = GetInstanceID(); 
        
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
    }
    public void SendCube()
    {
        rb.AddForce(transform.forward * 700);
    }
    public void CreatingForce()
    {
        rb.AddForce((Vector3.up - Vector3.right) * 150);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Cube"))
        {
            if(col.gameObject.TryGetComponent(out CubeClass cube))
            {
                if(cube.value==value)
                {
                    if (ID < cube.ID) return; 
                    Destroy(this.gameObject); 
                    Destroy(col.gameObject);
                    if(nextCube!=null) 
                    {
                        GameObject temp = Instantiate(nextCube, transform.position, Quaternion.identity); 
                        if(temp.TryGetComponent(out CubeClass newCube)) 
                        {
                            newCube.CreatingForce();
                        }

                    }
                }
            }
        }
    }

    void Update()
    {
        
    }
}
