using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongTac : MonoBehaviour
{

    public float Autospeed;
    public float speed;
    public Vector3 vStart, vEnd, vitri1_1, vitri2_1, vitri1_2, vitri2_2;
    public Transform Tamvan1, TamVan2;

    // Start is called before the first frame update
    void Start()
    {
        vStart = transform.position;
        vitri1_1 = Tamvan1.position;
        vitri1_2 = TamVan2.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (isMovingDown)
        {

            transform.position = Vector3.MoveTowards(transform.position, vEnd, speed * Time.deltaTime);
        }


        if (isMovingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, vStart, speed * Time.deltaTime);
        }
        MoveTamvanTim();
        MoveTamVanXanh();
        MoveTamvantim2();
        /////
        if (!yeallow) return;
        //if (!ismovingright) return;
        //if (!ismovingleft) return;
        MoveTamVanVang();

    }


    private bool isMovingDown, isMovingUp, isTimup, isMovingLeft, isMovingRight, isMovingLeft2, isMovingRight2, isTimdown2, isTimup2;
    public bool yeallow, red, red2, green, green2, isTimdown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))


            isMovingDown = true;
            isMovingUp = false;
       


        if (yeallow)
        {
            //// dieu khien tan van
            isMovingRight = true;
            isMovingLeft = false;
          
        }
        if (red)
        {

            isTimup = true;
            isTimdown = false;

        }
        if (green)
        {
            isTimdown = true;
            isTimup = false;
        }
        if (green2)
        {
            isMovingLeft2 = true;
            isMovingRight2 = false;
        }
        if (red2)
        {
            isTimup2 = true;
            isTimdown2 = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))

        {

            isMovingDown = false;
            isMovingUp = true;

            if (yeallow)
            {
                isMovingRight = false;
                isMovingLeft = true;

            }
            if (red)
            {

                isTimdown = true;
                isTimup = false;
            }
            if (green)
            {
                isTimdown = false;
                isTimup = true; ;
            }
            if (green2)
            {
                isMovingLeft2 = false;
                isMovingRight2 = true;
            }
            if (red2)
            {
                isTimup2 = false;
                isTimdown2 = true;
            }
        }
    }

    public void MoveTamVanVang()
    {

        if (isMovingRight)
        {
            TamVan2.position = Vector3.MoveTowards(TamVan2.position, vitri2_2, Autospeed * Time.deltaTime);
        }

        if (isMovingLeft)
        {
            TamVan2.position = Vector3.MoveTowards(TamVan2.position, vitri1_2, Autospeed * Time.deltaTime);
        }
    }
    public void MoveTamvanTim()
    {
        if (isTimdown)
        {
            Tamvan1.position = Vector3.MoveTowards(Tamvan1.position, vitri2_1, Autospeed * Time.deltaTime);

        }
        if (isTimup)
        {
            Tamvan1.position = Vector3.MoveTowards(Tamvan1.position, vitri1_1, Autospeed * Time.deltaTime);

        }
    }
    public void MoveTamVanXanh()
    {

        if (isMovingRight2)
        {
            TamVan2.position = Vector3.MoveTowards(TamVan2.position, vitri1_2, Autospeed * Time.deltaTime);
        }

        if (isMovingLeft2)
        {
            TamVan2.position = Vector3.MoveTowards(TamVan2.position, vitri2_2, Autospeed * Time.deltaTime);
        }
    }
    public void MoveTamvantim2()
    {
        if (isTimdown2)
        {
            TamVan2.position = Vector3.MoveTowards(TamVan2.position, vitri1_2, Autospeed * Time.deltaTime);
        }
        if (isTimup2)
        {
            TamVan2.position = Vector3.MoveTowards(TamVan2.position, vitri2_2, Autospeed * Time.deltaTime);
        }
    }
}
