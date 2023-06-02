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
    public float kamerahýz;
    public float hýz = 0.1f;
   
    void Start()
    {
        //kamera atamalarý
        kamerahýz = 20;
        kamera = this.transform;

        //oyuncda fare imlecini kitlenmesi
        Cursor.lockState = CursorLockMode.Locked;
      
    }

    
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        //belli bir hýzda kameranýn oyuncuyu takip etmesi
        this.transform.position = Vector3.Lerp(this.transform.position, player.position + hedefmesafe, Time.deltaTime * kamerahýz);

        //fare sað sol yukarý aþaðý deðerlerinin atanmasý
        farex += Input.GetAxis("Mouse X") * farehassasiyeti;
        farey -= Input.GetAxis("Mouse Y") * farehassasiyeti;
       // farex = Mathf.Clamp(farex, -75, 75);

       //yukarý aþaðý açý kýsýtlamasý
        farey = Mathf.Clamp(farey, -30, 75);

        //kameranýn sað sol yukarý aþaðý hareketini saðlama
        this.transform.eulerAngles = new Vector3(farey, farex, 0);
        
        //eyer w ileri veya niþan alýrken kamera ile birlikte oyuncununda o yöne dönmesi
         if(Input.GetKey(KeyCode.W) || Input.GetMouseButton(1))
         {
            player.rotation = Quaternion.Slerp(player.rotation, kamera.rotation, Time.deltaTime *10);

         }

         

    }
   
  
}
