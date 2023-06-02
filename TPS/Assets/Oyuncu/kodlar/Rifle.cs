using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.U2D;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    float ate�etmearal��� = 0.1f;
    float ate�etmezaman� = 0.0f;
    float sarjordekimermisay�s� = 24;
    float depokur�un = 30;
    GameObject oyuncu;
    GameObject d��man;
    Camera kamera;
    Animator animator;
    Transform rifle;
    BoxCollider silah;
    void Start()
    {
        //GameObje ve transformlara atamalar yap�l�yor
        d��man = GameObject.FindWithTag("d��man");
        silah = this.GetComponent<BoxCollider>();    
        oyuncu = GameObject.FindWithTag("Player");
        kamera = Camera.main;
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        rifle = this.transform;
    }

    
    void Update()
    {
       
       //Fare de sa� t�k ile ni�an alma 
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("nisan", true);
             
            //Fare de sa� t�k bas�l� iken sol t�k ile  belli aral�klarla ate� etme
            if (Input.GetMouseButton(0))
            {
                if (Time.time >= ate�etmezaman�)
                {
                    if (sarjordekimermisay�s� > 0)
                    {
                        AtesEt();
                        ate�etmezaman� = ate�etmearal��� + Time.time;
                        
                    }
                    
                }
            }
            else
            {
                animator.SetBool("isshoot", false);
                
            }
        }
        //Fare ile sol t�k bas�ld���nda  elinde silah varsa melee attack ger�ekle�tirme
        else if(Input.GetMouseButton(0) && oyuncu.GetComponent<Oyuncuhareket>().elindenevar == 0)
        {
            silah.enabled = true;
            
            animator.SetBool("rifleattack", true);
        }
        else
        {
            //attack yap�lmad���nda animasyonlar�n kapat�lmas� ve silah�n box colider�n�n kapat�lmas�
            animator.SetBool("nisan", false);
            animator.SetBool("rifleattack", false);
            animator.SetBool("isshoot", false);
            silah.enabled = false;
        }
       
    }
    public void OnCollisionEnter(Collision collision)
    {
        //melee attack s�ras�nda d��man�n hasar g�rmesi
        d��man.GetComponent<d��man>().HasarAl(5);
    }
    
    public void AtesEt()
    {
        //bir yapay ���n olu�turmak ve ate� animasyonun aktifle�tirilmesi
        animator.SetBool("isshoot", true);
        Ray ray = kamera.ViewportPointToRay(new Vector3(0.4f, 0.5f, 0));
        RaycastHit hit;
        
        //���n�n g�derilmesi
        if (Physics.Raycast(ray, out hit))
        {
           
            if (hit.collider.gameObject.tag == "d��man")
            {
                //eyer ���k d��man etiketli objeye �arparsa c�n�n�n aazalt�lmas�
                hit.collider.gameObject.GetComponent<d��man>().HasarAl(10);
            }
        }
    }
}
