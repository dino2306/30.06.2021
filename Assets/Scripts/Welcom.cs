using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Welcom : MonoBehaviour
{
    public GameObject Panel_seLecMap;

    public GameObject Panel_Level_Easy;
    public GameObject Panel_Level_Easy2;
    public GameObject Panel_Level_Normal;
public GameObject Panel_Level_Normal2;

    public Image Sound;
    public Sprite on, off;
  
    public string Scencename;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      // btnlevel.sprite = Unlock;
       
    }
    public void Open(int ScenID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(ScenID);
    }

    public void ChoosePlay()
    {
       
            Panel_seLecMap.SetActive(true);
        
    }

    public void Easy()
    {
        Panel_Level_Easy.SetActive(true);
        Panel_seLecMap.SetActive(false);
    }

    public void Normal()
    {
        Panel_Level_Normal.SetActive(true);
        Panel_seLecMap.SetActive(false);
    }
    public void Next()
    {
        Panel_Level_Easy2.SetActive(true);
        Panel_Level_Easy.SetActive(false);
    }

    public void Back()
    {
        Panel_Level_Easy2.SetActive(false);
        Panel_Level_Easy.SetActive(true);
    }

    public void Next2()
    {
        Panel_Level_Normal2.SetActive(true);
        Panel_Level_Normal.SetActive(false);
    }

    public void Back2()
    {
        Panel_Level_Normal2.SetActive(false);
        Panel_Level_Normal.SetActive(true);
    }


    public bool ischange;
    public void Music()
    {
        if (ischange)
        {
            Sound.sprite = on;
        }
        else
        {
            Sound.sprite = off;
        }
        ischange = !ischange;
    }

    public void Thoat()
    {
        SceneManager.LoadScene(Scencename);
    }
}
