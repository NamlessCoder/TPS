using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Kamerakonrtol : MonoBehaviour
{
    public float farehassasiyeti = 2f;
    float farex, farey;
    public Transform player;
    public Transform kamera;
    public float t;
    public Vector3 hedefmesafe;
    public float kamerah�z;
    public float h�z = 0.1f;
   
    void Start()
    {
        //kamera atamalar�
        kamerah�z = 20;
        kamera = this.transform;

        //oyuncda fare imlecini kitlenmesi
        Cursor.lockState = CursorLockMode.Locked;
      
    }

    
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        //belli bir h�zda kameran�n oyuncuyu takip etmesi
        this.transform.position = Vector3.Lerp(this.transform.position, player.position + hedefmesafe, Time.deltaTime * kamerah�z);

        //fare sa� sol yukar� a�a�� de�erlerinin atanmas�
        farex += Input.GetAxis("Mouse X") * farehassasiyeti;
        farey -= Input.GetAxis("Mouse Y") * farehassasiyeti;
       // farex = Mathf.Clamp(farex, -75, 75);

       //yukar� a�a�� a�� k�s�tlamas�
        farey = Mathf.Clamp(farey, -30, 75);

        //kameran�n sa� sol yukar� a�a�� hareketini sa�lama
        this.transform.eulerAngles = new Vector3(farey, farex, 0);
        
        //eyer w ileri veya ni�an al�rken kamera ile birlikte oyuncununda o y�ne d�nmesi
         if(Input.GetKey(KeyCode.W) || Input.GetMouseButton(1))
         {
            player.rotation = Quaternion.Slerp(player.rotation, kamera.rotation, Time.deltaTime *10);

         }

         

    }
   
  
}
