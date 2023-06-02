using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatorkontroller : MonoBehaviour
{
   Animator kontroller;
    void Start()
    {
        kontroller = GetComponent<Animator>();
    }

    
    void Update()
    {
        //harekete baðlý animasyon aktifleþtirme
        if (Input.GetKey(KeyCode.W)){
            kontroller.SetBool("isriflewalking", true);
        }
           
        else
            kontroller.SetBool("isriflewalking", false);
    }
}
