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
    bool �sGrounded;
    public Transform GroundCheck;
    public LayerMask Ground;
    //temel degerlerin atanmas�
    public float speed = 12f;
    public float Mass = 10f;
    public float gravity = -9.81f;
    public float GroundDistance = 0.4f;
    public float jumpforce = 10f;
    public float can = 100f;
    public Image canbar�;
    public Text canbasmag�sterge;
    public int cancubuk = 3;
    public int elindenevar;

    void Start()
    {
        animator = GetComponent<Animator>();
        CC = GetComponent<CharacterController>();
        //bu deger oyuncunun silah veye bo� elde oldugunu g�steren deger
        elindenevar = 0;
    }

    
    void Update()
    {
        //Oyuncunun elinin bo� veya silahl� oldu�unun kontrol�
        ElKontrol();

        //oyuncunun yerde oldup olmad���n�n kontrol�
        �sGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance,Ground);

        //oyuncunun e�er yerdeysebelli bir yer�ekimi etkisine girer
        if (velocity.y < 0 && �sGrounded)
        {
            velocity.y = -1;
        }

        //E�er space tu�una bas�l�rsa ve oyuncu yerde ise oyuncu z�plar
        if (Input.GetKeyDown(KeyCode.Space) && �sGrounded)
        {
            velocity.y += Mathf.Sqrt(gravity * -2 * jumpforce);
        }
        
        //Eyer c ye bas�l�rsa oyuncu can basar
        if(Input.GetKeyDown(KeyCode.C))
        {
            CanBas();
        }

        //oyuncunun can�  alt�ndaysa �l�r
        if(can <= 0)
        {
            animator.SetBool("death", true);
            StartCoroutine(YokOl());
        }
        //hareket etmeye yarayan fonksiyon
        else
            Hareket();
        //Eyer oyuncunun eli bo� ve sol t�k basarsa melee attack ger�ekle�tirir
        if (Input.GetMouseButton(0) && elindenevar == 1)
        {
            animator.SetBool("attack", true);
            //el bo� oldu�unda silah deaktif olur
            rifle.SetActive(false);
        }
        else if(elindenevar == 0)
        {
            //oyuncu silah se�ti�inde silah aktif olur
            rifle.SetActive(true);
        }
        else
        {
            animator.SetBool("attack", false);
            
        }
           
    }
    public void CanBas()
    {
        //can basacak cubuklar s�f�rdan y�ksek ise can vas�l�r ve de�i�iklikler ekranda gzsterilir
        if (cancubuk > 0)
        {
            can += 50;
            cancubuk -= 1;
            canbasmag�sterge.text = cancubuk.ToString();
            canbar�.fillAmount += 0.5f;
        }
    }
    public void CanCubukAlma()
    {
        //herhangi bir yerden can al�nd���nda can basma cun-buklar� artar ve ekranda g�ssterilir
        cancubuk += 1;
        canbasmag�sterge.text = cancubuk.ToString();
    }
    public void ElKontrol()
    {
        //oyuncun 1 tu�u ile eline silah almas�
        if(Input.GetKeyDown(KeyCode.Alpha1)) { 
            elindenevar = 0;
        }
        //oyuncunun 2 tu�u ile bo� ele ge�mesi
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            elindenevar = 1;
        }
    }
   public void HasarAl(int hsr)
    {
        //oyuncunun hasar almas�n� sa�layan fonk.
        can -= hsr;
        canbar�.fillAmount -= 0.1f;
       
    }
    void Hareket()
    {
        //klavyeden al�ndan girdilerle x ve y bile�enine yatay ve dikey hareketlerin atanmas�
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //y�r�me animasyon aktifle�tirme
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
        //oyuncu �ld�kten 5 sn sonra yok olmas�
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
