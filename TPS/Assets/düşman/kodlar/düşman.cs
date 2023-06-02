using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class düşman : MonoBehaviour
{
    public float DHP = 100f;
    float ateşetmearalığı = 1.2f;
    float ateşetmezamanı = 0.0f;
    Animator animator;
    bool isdead;
    NavMeshAgent dusmannavmesh;
    float mesafe;
    Vector3 target;
    GameObject hedefoyuncu;
    public BoxCollider bc;
    public GameObject can;


    void Start()
    {
        animator = GetComponent<Animator>();
        dusmannavmesh = GetComponent<NavMeshAgent>();
        hedefoyuncu = GameObject.FindWithTag("Player");
        
        
    }


    void Update()
    {
       //düşman ile oyuncu arasındaki mesafenin ölçümü
        mesafe = Vector3.Distance(this.transform.position,hedefoyuncu.transform.position);

        animator.SetBool("istakedamage", false);
        
     //Eyer düşmanın canı 0 ve altındaysa ölüm animasyonu aktif olur ve yok olur
        if (DHP <= 0)
        {
            dusmannavmesh.isStopped = true;
            animator.SetBool("isdead", true);
            isdead = true;
            StartCoroutine(YokOl());
            

        }
        else
        {
            //Eyer oyuncu ile arasındaki mesafe 3,3 den küçükse belli aralıklarla attack yapması
            if (mesafe <= 3.3f)
            {
                animator.SetBool("isattack1", false);
                if (Time.time >= ateşetmezamanı)
                {

                    Attack();
                    ateşetmezamanı = ateşetmearalığı + Time.time;

                }
                
               
                
            }
                //Eyer düşman ile oyuncu mesafesi açılırsa düşmana oyuncunn konumu verilir ve düşman oraya doğru ilerler
            else if (mesafe <= 10)
                Kovalama(); 
                

        }
        //Eyer düşman oyuncuyun son gördüğü konuma ulaşırsa orada bekler 
        if (this.transform.position.x == target.x)
        {
            animator.SetBool("iswalk", false);
            dusmannavmesh.isStopped = true;
        }
    }
    public void Attack()
    {
       //düşmanın elindeki colider aktifleşir
        bc.enabled = true;
        //düşman oyuncuya doğru bakar
        this.transform.LookAt(hedefoyuncu.transform);

        //gerekli animasyon atamaları yapılır
        animator.SetBool("iswalk", false);
        dusmannavmesh.isStopped = true;

        animator.SetBool("isattack1", true);
       


    }

    private void OnTriggerEnter(Collider other)
    {
        //Eyer düşman oyuncuya vurursa cını azalır
        if(other.gameObject.tag == "Player")
        {
            hedefoyuncu.GetComponent<Oyuncuhareket>().HasarAl(10);
        }
    }
    public void Kovalama()
    {
        //düşmanın colidrr kapatılır 
        bc.enabled = false;
        //oyuncuya bakar 
        this.transform.LookAt(hedefoyuncu.transform);
        //animator.SetBool("isattack1", false);
        animator.SetBool("iswalk", true);

        //düşman oyuncuya doğru harekete geçer
        dusmannavmesh.isStopped = false;
        dusmannavmesh.SetDestination(hedefoyuncu.transform.position);
        target = hedefoyuncu.transform.position;

    }
    IEnumerator YokOl()
    {
        //düşmanın canı 0 ın altına düşünce yok olur ve ardında toplanabilen can bırakır
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        Instantiate(can, this.transform.position +this.transform.up, Quaternion.identity);
    }
   
    public void HasarAl(int damage)
    {
        //düşmanın hasar alma animasyonu aktifleşir ve canı azalır
        dusmannavmesh.isStopped = false;
        animator.SetBool("iswalk", true);
        //düşman hasar alınca mesafe önemsiz şekilde oyuncuya doğru hareket eder
        dusmannavmesh.SetDestination(hedefoyuncu.transform.position);
        target = hedefoyuncu.transform.position;
        DHP -= damage;
        animator.SetBool("istakedamage", true);
    }
}
