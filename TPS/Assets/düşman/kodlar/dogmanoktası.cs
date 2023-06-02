using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogmanoktası : MonoBehaviour
{
    public GameObject düşman;
    void Start()
    {
        //düşman oluşturma 
        GameObject yenidüşman = Instantiate(düşman,this.transform.position,Quaternion.identity);
    }

    
    void Update()
    {
        
    }
}
