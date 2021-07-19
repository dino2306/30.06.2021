using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerChoose : MonoBehaviour
{
   // public ggAdmos gg;                      //quangcao
    public MoveAni _player1, _player2;
  //  public ListCongTac check_1, check_2;
    public GameObject BtnSwapBlue;
    public GameObject BtnSwapRed;
    public GameObject Panel_Win;

    public GameObject PausePanel;
    public winMission winmission;
    public FollowCamera flCam;

    public Color clRed, clBlue;
    public Image imBg, jumpBtn, leftBtn, rightBtn;
    private Color RGBColor;

    public string Map2;
    public string welcomHome;

    public int missionId;

    public string play2;

  
    // Start is called before the first frame update
    void Start()
    {
        imBg.color = clBlue;
        PlayerPrefs.GetString("Player", "Play1").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        SwapBlueKey();
        SwapRedkey();
        HandPause();
        StartCoroutine( NexMapp2xx());
        if (Input.GetKey(KeyCode.W))
        {
            _player1.Opening = true;
            _player2.Opening = true;
        }
        //  winmission.UnlockNextMission(missionId);
        if (play2 != PlayerPrefs.GetString("Player", "Play1"))
        {
            PlayerPrefs.SetString("Player", play2);
        }
       

    }


    public void SwapRed() ///// click button
    {
        _player1.choose = false;
        _player2.choose = true;

        imBg.color = clRed;

        BtnSwapBlue.SetActive(false);
        BtnSwapRed.SetActive(true);

        _player1.StopMoving();
        flCam.Check = false;    //check camera

        //check_1.check = false;   // check cong tac
        //check_2.check = true;
      

    }

    public void SwapBlue()
    {
        _player1.choose = true;
        _player2.choose = false;

        imBg.color = clBlue;

        BtnSwapBlue.SetActive(true);
        BtnSwapRed.SetActive(false);

        _player2.StopMoving();
        flCam.Check = true;

        //check_1.check = true;
        //check_2.check = false;
      
    }

    private void SwapBlueKey()      ////click key
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            _player1.choose = true;
            _player2.choose = false;

            imBg.color = clBlue;

            BtnSwapBlue.SetActive(true);
            BtnSwapRed.SetActive(false);

            _player2.StopMoving();
            _player2.PlayAninmation(_player2.happy);

            flCam.Check = true;

            //check_1.check = true;
            //check_2.check = false;
        }
    }

    private void SwapRedkey()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {


            _player1.choose = false;
            _player2.choose = true;

            imBg.color = clRed;

            BtnSwapBlue.SetActive(false);
            BtnSwapRed.SetActive(true);

            _player1.StopMoving();
           _player1.PlayAninmation(_player1.happy);

            flCam.Check = false;

            //check_1.check = false;   // check cong tac
            //check_2.check = true;

        }
    }

    public void home()
    {
        SceneManager.LoadScene(welcomHome);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
       Time.timeScale = 0;
    }

    public void HandPause()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resum()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void Reset(int sceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneID);
       
    }
    void Reset2()
    {
        if (_player2.choose)
        {
            Debug.Log("Red");
            SwapRed();
        }
        if (_player1.choose)
        {
            Debug.Log("blue");
            SwapBlue();
        }
    }
    

    IEnumerator NexMapp2xx()
    {
       
        if (_player1.Opening && _player2.Opening )
        {
            _player1.PlayAninmation(_player1.Win);
            _player2.PlayAninmation(_player1.Win);
            yield return new WaitForSeconds(1.5f);
            Panel_Win.SetActive(true);
           // gg.GameOver();

            winmission.UnlockNextMission(missionId);        //UnlockMap
        }
       
    }
    public void Nextmap()
    {
        SceneManager.LoadScene(Map2);
    }

    public void HightjumpBtn()
    {
        RGBColor.a = 1;
        RGBColor.r = 1;
        RGBColor.g = 1;
        RGBColor.b = 1;
       jumpBtn.color = RGBColor;
    }
    public void LowBtn()
    {
        RGBColor.a = 0.3f;
        RGBColor.r = 1;
        RGBColor.g = 1;
        RGBColor.b = 1;
        jumpBtn.color = RGBColor;
    }

    public void HightLeftbtn()
    {

        RGBColor.a = 1;
        RGBColor.r = 1;
        RGBColor.g = 1;
        RGBColor.b = 1;
       leftBtn.color = RGBColor;
    }
    public void LowLeftBtn()
    {
        RGBColor.a = 0.3f;
        RGBColor.r = 1;
        RGBColor.g = 1;
        RGBColor.b = 1;
        leftBtn.color = RGBColor;
    }
    public void HightRightBtn()
    {

        RGBColor.a = 1;
        RGBColor.r = 1;
        RGBColor.g = 1;
        RGBColor.b = 1;
       rightBtn.color = RGBColor;
    }
    public void LowRightBtn()
    {
        RGBColor.a = 0.3f;
        RGBColor.r = 1;
        RGBColor.g = 1;
        RGBColor.b = 1;
       rightBtn.color = RGBColor;
    }

}







