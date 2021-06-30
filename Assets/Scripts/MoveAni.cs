using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MoveAni : MonoBehaviour
{
    public bool choose, check, Opening;

    public GameController gamecontroller;
    public CongAnimation dooranmation;

    public SkeletonAnimation anim;
    private Rigidbody2D rb;

    public float speed;
    public float jumpForce;

//Kiem tra va cham --> Jump
    bool isGrounded;
    public Transform groundCehck;
    public LayerMask groundLayer;

    //kiem tra di chuyen
    private bool moveRight, moveleft, die, isMovingDown, isMovingUp;
    //load scene
    public string Scenename;

    public Button right, left, jump;
   

    [SpineAnimation] public string IdleAnim; //cac hanh dong
    [SpineAnimation] public string walkAnim;
    [SpineAnimation] public string jumpAnim;
    [SpineAnimation] public string isDie;
    [SpineAtlasRegion] public SpineSkin skin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       moveRight = false;
        moveleft = false;
        GetComponent<Image>();
     
    }
    // Update is called once per frame
    void Update()
    {

        // Nextmap();
        if (!choose) return;
        Handmove();
        HandJump();
        MoveManage();

        //checkChamdat
        isGrounded = Physics2D.OverlapCircle(groundCehck.position, 0.2f, groundLayer);

       
    }

    private void PlayAninmation(string _strAnim)
    {
        if (!anim.AnimationName.Equals(_strAnim))
        {

            anim.AnimationState.SetAnimation(0, _strAnim, true);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (check)
        {
            if (collision.gameObject.tag == "Dimon2")
            {
                Destroy(collision.gameObject);
                gamecontroller.getScore2();
            }
            if (collision.gameObject.tag == "Finish2" && gamecontroller.num2 == 0 )
            {
                dooranmation.OpenDoor2();
                Opening = true;
            }
            else
            {
                dooranmation.CloseDoor2();
                Opening = false;
            }
            if (collision.gameObject.tag == "Dieblue")
            {
                rb.velocity = new Vector3(0f, 0f);
                Died();
                die = true;
                
                PlayAninmation(isDie);
                StartCoroutine(Dieing());

            }
            if (collision.gameObject.tag == "Die")
            {
                rb.velocity = new Vector3(0f, 0f);
                Died();
                die = true;
                PlayAninmation(isDie);
                StartCoroutine(Dieing());
            }

        }
        else
        {
            if (collision.gameObject.tag == "Dimon")
            {
                Destroy(collision.gameObject);
                gamecontroller.getScore1();
            }
            if (collision.gameObject.tag == "Finish" && gamecontroller.num == 0)
            {
                dooranmation.OpenDoor1();
                Opening = true;
            }
            else
            {
                dooranmation.CloseDoor1();
                Opening = false;
            }
            if (collision.gameObject.tag == "DieRed")
            {
                rb.velocity = new Vector3(0f, 0f);
                Died();
                die = true;
                PlayAninmation(isDie);               
                StartCoroutine(Dieing());
                
            }
            if (collision.gameObject.tag == "Die")
            {
                rb.velocity = new Vector3(0f, 0f);
                Died();
                die = true;
                PlayAninmation(isDie);
                StartCoroutine(Dieing());
            }

        }
    }

    private void MoveManage()
    {

        if (moveRight)
        {
            rb.velocity = new Vector2(+speed, rb.velocity.y);
            anim.skeleton.ScaleX = 1;
            if (isGrounded)
            {
                PlayAninmation(walkAnim);
            }
        }

        if (moveleft)
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y);
            anim.skeleton.ScaleX = -1;
            if (isGrounded)
            {
                PlayAninmation(walkAnim);
            }
        }

    }
    public void MoveLeft()
    {
        if (!die)
        {
            moveleft = true;
        }
        
    }

    public void MoveRight()
    {
        if (!die)
        {
            moveRight = true;
        }
      
      
    }

    public void Jump()
    {
        if (!choose) return;
        if (!die)
        {
            if (isGrounded)
            {
                if (rb.velocity.y == 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    PlayAninmation(jumpAnim);
                }
            }
        }
       
        
    }
    public void stopjum()
    {
        if (!die)
        {
            PlayAninmation(IdleAnim);
        }
    }

    public void StopMoving()
    {
        if (!die)
        {
            moveleft = false;
            moveRight = false;
            rb.velocity = Vector2.zero;
            if (isGrounded)
            {

                PlayAninmation(IdleAnim);
            }
        }
    }

    public void Handmove()
    {
        if (!die)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(+speed, rb.velocity.y);
                // anim.skeleton.FlipX = false;
                anim.skeleton.ScaleX = 1;

                if (isGrounded)
                {
                    PlayAninmation(walkAnim);
                }
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                PlayAninmation(IdleAnim);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                //  PlayAninmation(IdleAnim);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                // anim.skeleton.FlipX = true;
                anim.skeleton.ScaleX = -1;
                if (isGrounded)
                {
                    PlayAninmation(walkAnim);
                }
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                PlayAninmation(IdleAnim);
            }
        }
    }
    bool dbjump;
    private void HandJump()
    {
        if (!die)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    if (rb.velocity.y == 0)
                    {
                        rb.velocity = Vector2.up * jumpForce;
                        PlayAninmation(jumpAnim);
                        dbjump = true;
                    }
                    if (dbjump)
                    {
                        rb.velocity = Vector2.up * jumpForce;
                       
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                PlayAninmation(IdleAnim);
            }
        }
    }

   IEnumerator Dieing( )
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(Scenename);
    }

    public void Died()
    {
        moveleft = false;
        moveRight = false;
    }

//////////Hieu ung khi clickbuton


    
}

    

   


