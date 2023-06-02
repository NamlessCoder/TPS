using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oyuncuhareket : MonoBehaviour
{
    public GameObject rifle;
    Animator animator;
    CharacterController CC;
    Vector3 velocity, move;
    bool ÝsGrounded;
    public Transform GroundCheck;
    public LayerMask Ground;
    //temel degerlerin atanmasý
    public float speed = 12f;
    public float Mass = 10f;
    public float gravity = -9.81f;
    public float GroundDistance = 0.4f;
    public float jumpforce = 10f;
    public float can = 100f;
    public Image canbarý;
    public Text canbasmagösterge;
    public int cancubuk = 3;
    public int elindenevar;

    void Start()
    {
        animator = GetComponent<Animator>();
        CC = GetComponent<CharacterController>();
        //bu deger oyuncunun silah veye boþ elde oldugunu gösteren deger
        elindenevar = 0;
    }

    
    void Update()
    {
        //Oyuncunun elinin boþ veya silahlý olduðunun kontrolü
        ElKontrol();

        //oyuncunun yerde oldup olmadýðýnýn kontrolü
        ÝsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance,Ground);

        //oyuncunun eðer yerdeysebelli bir yerçekimi etkisine girer
        if (velocity.y < 0 && ÝsGrounded)
        {
            velocity.y = -1;
        }

        //Eðer space tuþuna basýlýrsa ve oyuncu yerde ise oyuncu zýplar
        if (Input.GetKeyDown(KeyCode.Space) && ÝsGrounded)
        {
            velocity.y += Mathf.Sqrt(gravity * -2 * jumpforce);
        }
        
        //Eyer c ye basýlýrsa oyuncu can basar
        if(Input.GetKeyDown(KeyCode.C))
        {
            CanBas();
        }

        //oyuncunun caný  altýndaysa ölür
        if(can <= 0)
        {
            animator.SetBool("death", true);
            StartCoroutine(YokOl());
        }
        //hareket etmeye yarayan fonksiyon
        else
            Hareket();
        //Eyer oyuncunun eli boþ ve sol týk basarsa melee attack gerçekleþtirir
        if (Input.GetMouseButton(0) && elindenevar == 1)
        {
            animator.SetBool("attack", true);
            //el boþ olduðunda silah deaktif olur
            rifle.SetActive(false);
        }
        else if(elindenevar == 0)
        {
            //oyuncu silah seçtiðinde silah aktif olur
            rifle.SetActive(true);
        }
        else
        {
            animator.SetBool("attack", false);
            
        }
           
    }
    public void CanBas()
    {
        //can basacak cubuklar sýfýrdan yüksek ise can vasýlýr ve deðiþiklikler ekranda gzsterilir
        if (cancubuk > 0)
        {
            can += 50;
            cancubuk -= 1;
            canbasmagösterge.text = cancubuk.ToString();
            canbarý.fillAmount += 0.5f;
        }
    }
    public void CanCubukAlma()
    {
        //herhangi bir yerden can alýndýðýnda can basma cun-buklarý artar ve ekranda gössterilir
        cancubuk += 1;
        canbasmagösterge.text = cancubuk.ToString();
    }
    public void ElKontrol()
    {
        //oyuncun 1 tuþu ile eline silah almasý
        if(Input.GetKeyDown(KeyCode.Alpha1)) { 
            elindenevar = 0;
        }
        //oyuncunun 2 tuþu ile boþ ele geçmesi
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            elindenevar = 1;
        }
    }
   public void HasarAl(int hsr)
    {
        //oyuncunun hasar almasýný saðlayan fonk.
        can -= hsr;
        canbarý.fillAmount -= 0.1f;
       
    }
    void Hareket()
    {
        //klavyeden alýndan girdilerle x ve y bileþenine yatay ve dikey hareketlerin atanmasý
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //yürüme animasyon aktifleþtirme
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", z);

        //oyuncuyu hareker-t ettirme
        move = transform.right * x + transform.forward * z;
        CC.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime * Mass;
        CC.Move(velocity * Time.deltaTime);
        
    }
    IEnumerator YokOl()
    {
        //oyuncu öldükten 5 sn sonra yok olmasý
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
