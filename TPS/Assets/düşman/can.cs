using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class can : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        //tag ile player GameObjesine atama yapma
        player = GameObject.FindWithTag("Player");
        
    }

    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        //eyer canýn etkileþime girdiði þeyin tagý player ise 
        if(other.gameObject.tag == "Player")
        {
            //oyuncuya can bas sonra yok ol
            player.GetComponent<Oyuncuhareket>().CanCubukAlma();
            Destroy(this.gameObject);
        }
        
       
    }
   
}
