using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elattack : MonoBehaviour
{
    GameObject düþman;
    void Start()
    {
        düþman = GameObject.FindWithTag("düþman");
    }


    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "düþman")
        {
            //oyuncu boþ el ile attack sýrasýnda düþmana hasar verme
            düþman.GetComponent<düþman>().HasarAl(5);
        }
    }
   
}
