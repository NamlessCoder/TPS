using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elattack : MonoBehaviour
{
    GameObject d��man;
    void Start()
    {
        d��man = GameObject.FindWithTag("d��man");
    }


    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "d��man")
        {
            //oyuncu bo� el ile attack s�ras�nda d��mana hasar verme
            d��man.GetComponent<d��man>().HasarAl(5);
        }
    }
   
}
