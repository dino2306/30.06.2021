using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using GoogleAdMod;
public class MoveAni : MonoBehaviour
{
    public ggAdmos gg;
    public FollowCamera flcam;                                 //Quang cao
    public bool choose, check, Opening, die, eventbtndow;

    public GameController gamecontroller;
    public CongAnimation dooranmation;


    public GameObject Panel_GameOver;

    public SkeletonAnimation anim;
    private Rigidbody2D rb;

    public float speed;
    public float jumpForce;

    //Kiem tra va cham --> Jump
    bool isGrounded;
    public Transform groundCehck;
    public LayerMask groundLayer;

    //kiem tra di chuyen
    private bool moveRight, moveleft;
    //load scene
    // public string Scenename;

    [SpineAnimation] public string IdleAnim; //cac hanh dong
    [SpineAnimation] public string walkAnim;
    [SpineAnimation] public string jumpAnim;
    [SpineAnimation] public string isDie;
    [SpineAnimation] public string happy;
    [SpineAnimation] public string Win;

    [SpineAtlasRegion] public SpineSkin skin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveRight = false;
        moveleft = false;
        GetComponent<Image>();

        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.acVideoComplete += HandleVideoReward;
        }
    }
    private void OnDisable()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.acVideoComplete -= HandleVideoReward;

        }
    }
    private void HandleVideoReward()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    // Update is called once per frame
    void Update()
    {

        // Nextmap();
        if (!choose) return;
        Handmove();
        //HandMove2();
        HandJump();
        MoveManage();

        isGrounded = Physics2D.OverlapCircle(groundCehck.position, 0.2f, groundLayer);



    }

    public void PlayAninmation(string _strAnim)
    {
        if (!anim.AnimationName.Equals(_strAnim))
        {

            anim.AnimationState.SetAnimation(0, _strAnim, true);
        }
    }
    public void PlayAninmationDie(string _strAnim)
    {
        if (!anim.AnimationName.Equals(_strAnim))
        {

            anim.AnimationState.SetAnimation(0, _strAnim, false);
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
            if (collision.gameObject.tag == "Finish2" && gamecontroller.num2 == 0)
            {
                dooranmation.OpenDoor2();
                Opening = true;

            }

            if (collision.gameObject.tag == "Dieblue")
            {
                rb.velocity = new Vector3(0f, 0f);
                Died();
                die = true;

                PlayAninmationDie(isDie);
                StartCoroutine(Dieing());
                // Panel_GameOver.SetActive(true);

            }
            if (collision.gameObject.tag == "Die")
            {
                rb.velocity = new Vector3(0f, 0f);
                Died();
                die = true;
                PlayAninmationDie(isDie);
                StartCoroutine(Dieing());
                //Panel_GameOver.SetActive(true);
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
            if (collision.gameObject.tag == "DieRed")
            {
                rb.velocity = new Vector3(0f, 0f);
                Died();
                die = true;
                PlayAninmationDie(isDie);
                StartCoroutine(Dieing());
                // Panel_GameOver.SetActive(true);

            }
            if (collision.gameObject.tag == "Die")
            {
                rb.velocity = new Vector3(0f, 0f);
                Died();
                die = true;
                PlayAninmationDie(isDie);
                StartCoroutine(Dieing());
                //Panel_GameOver.SetActive(true);
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (check)
        {
            if (collision.gameObject.tag == "Finish2")
            {
                dooranmation.CloseDoor2();
                Opening = false;

            }
        }
        else
        {
            if (collision.gameObject.tag == "Finish")
            {
                dooranmation.CloseDoor1();
                Opening = false;
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

        if (!die & flcam.btn_0)
        {
            moveleft = true;
            flcam.btn_0 = false;
            Debug.Log(flcam.btn_0);
        }
        eventbtndow = true;
    }

    public void MoveRight()
    {
        if (!die)
        {
            moveRight = true;
        }

        eventbtndow = true;
    }

    public void Jump()
    {
        if (!choose) return;
        if (!die)
        {
            if (isGrounded)
            {
                //if (rb.velocity.y == 0)

                rb.velocity = Vector2.up * jumpForce;
                PlayAninmation(jumpAnim);

            }

        }
        eventbtndow = true;

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
            rb.velocity = new Vector3(0, rb.velocity.y);
            // if (isGrounded)
            // {

            PlayAninmation(IdleAnim);
            //  }
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
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
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
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                PlayAninmation(IdleAnim);
            }

            if (Input.GetKey(KeyCode.H))
            {
                PlayAninmation(happy);
            }
            if (Input.GetKey(KeyCode.P))
            {
                PlayAninmation(Win);
            }
        }
    }
    private void HandMove2()
    {
        if (!die)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector3(speed, rb.velocity.y);
                anim.skeleton.ScaleX = 1;
                if (isGrounded)
                {
                    PlayAninmation(walkAnim);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    rb.velocity = new Vector3(-speed, rb.velocity.y);
                    anim.skeleton.ScaleX = -1;
                    if (isGrounded)
                    {
                        PlayAninmation(walkAnim);
                    }
                }
                else
                {
                    rb.velocity = new Vector3(0, rb.velocity.y);
                    PlayAninmation(IdleAnim);
                }

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
                    //if (rb.velocity.y == 0)

                    rb.velocity = Vector2.up * jumpForce;
                    PlayAninmation(jumpAnim);
                    dbjump = true;
                }
                else if (dbjump)
                {
                    jumpForce = jumpForce / 1.5f;
                    rb.velocity = Vector2.up * jumpForce;
                    PlayAninmation(jumpAnim);
                    dbjump = false;
                    jumpForce = jumpForce * 1.5f;

                }
            }

            else if (Input.GetKeyUp(KeyCode.Space))
            {
                PlayAninmation(IdleAnim);
            }
        }
    }

    IEnumerator Dieing()
    {
        yield return new WaitForSeconds(1.5f);
        //gg.CreateAndLoadRewardedAd();
        AdsManager.Instance.ShowVideoReward();
        //  QuanCaoAdMods.GameOver2();
        Panel_GameOver.SetActive(true);

    }

    public void Died()
    {
        moveleft = false;
        moveRight = false;
    }



}






