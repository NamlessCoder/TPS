using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.U2D;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    float ateþetmearalýðý = 0.1f;
    float ateþetmezamaný = 0.0f;
    float sarjordekimermisayýsý = 24;
    float depokurþun = 30;
    GameObject oyuncu;
    GameObject düþman;
    Camera kamera;
    Animator animator;
    Transform rifle;
    BoxCollider silah;
    void Start()
    {
        //GameObje ve transformlara atamalar yapýlýyor
        düþman = GameObject.FindWithTag("düþman");
        silah = this.GetComponent<BoxCollider>();    
        oyuncu = GameObject.FindWithTag("Player");
        kamera = Camera.main;
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        rifle = this.transform;
    }

    
    void Update()
    {
       
       //Fare de sað týk ile niþan alma 
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("nisan", true);
             
            //Fare de sað týk basýlý iken sol týk ile  belli aralýklarla ateþ etme
            if (Input.GetMouseButton(0))
            {
                if (Time.time >= ateþetmezamaný)
                {
                    if (sarjordekimermisayýsý > 0)
                    {
                        AtesEt();
                        ateþetmezamaný = ateþetmearalýðý + Time.time;
                        
                    }
                    
                }
            }
            else
            {
                animator.SetBool("isshoot", false);
                
            }
        }
        //Fare ile sol týk basýldýðýnda  elinde silah varsa melee attack gerçekleþtirme
        else if(Input.GetMouseButton(0) && oyuncu.GetComponent<Oyuncuhareket>().elindenevar == 0)
        {
            silah.enabled = true;
            
            animator.SetBool("rifleattack", true);
        }
        else
        {
            //attack yapýlmadýðýnda animasyonlarýn kapatýlmasý ve silahýn box coliderýnýn kapatýlmasý
            animator.SetBool("nisan", false);
            animator.SetBool("rifleattack", false);
            animator.SetBool("isshoot", false);
            silah.enabled = false;
        }
       
    }
    public void OnCollisionEnter(Collision collision)
    {
        //melee attack sýrasýnda düþmanýn hasar görmesi
        düþman.GetComponent<düþman>().HasarAl(5);
    }
    
    public void AtesEt()
    {
        //bir yapay ýþýn oluþturmak ve ateþ animasyonun aktifleþtirilmesi
        animator.SetBool("isshoot", true);
        Ray ray = kamera.ViewportPointToRay(new Vector3(0.4f, 0.5f, 0));
        RaycastHit hit;
        
        //ýþýnýn göderilmesi
        if (Physics.Raycast(ray, out hit))
        {
           
            if (hit.collider.gameObject.tag == "düþman")
            {
                //eyer ýþýk düþman etiketli objeye çarparsa cýnýnýn aazaltýlmasý
                hit.collider.gameObject.GetComponent<düþman>().HasarAl(10);
            }
        }
    }
}
