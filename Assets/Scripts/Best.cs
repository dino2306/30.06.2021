using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Best : MonoBehaviour
{
   public ListCongTac listCT1,ListCT2;

    //public bool checkCt, checkct2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    bool check= false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !check)
        {
           // if (!collision.gameObject.tag.Contains("Player"))
            {
                listCT1.isMovingDown = true;
                ListCT2.isMovingDown = true;

                listCT1.isMovingUp = false;
                ListCT2.isMovingUp = false;

                check = true;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.gameObject.tag.Equals("Player"))
        {
            listCT1.isMovingDown = false;
            ListCT2.isMovingDown = false;

            listCT1.isMovingUp = true;
            ListCT2.isMovingUp = true;
            check = false;
        }
    }
}
