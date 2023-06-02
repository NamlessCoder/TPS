using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canbarıartırma : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

   
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player) {


        }
    }
}
