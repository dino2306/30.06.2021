using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets;
using TMPro;
public class Choose_menu : MonoBehaviour
{
    public GameObject Panel_Skin;

    public GameObject Premium, Rescue;

    public Image Premium_high, Premium_low, Rescue_high, Rescue_low;

    public Change_character change_1, change_2;

    public Listspine list_1, list_2;

    public Button BuySkin, Tryskin, SelectSkin, SelectedSkin;

    public List<GameObject> change;

    public List<Button> image_btn;
    public List<GameObject> image_lock;

    private AudioSource audioS;

    public AudioClip click;

    public int n  ;
    public bool bought;
    public ListSkin[] list;
    public bool check;

    public TextMeshProUGUI Diamon_Money;

    // Start is called before the first frame update
    void Start()
    {
        int lastMission = GameSetting.GetLastMission;

        int updateDiamon = PlayerPrefs.GetInt("SUMDIAMON", 0);
        if (updateDiamon >= 10)
        {
            Diamon_Money.text = updateDiamon.ToString();
        }
        else
        {
            Diamon_Money.text = "0 " + updateDiamon.ToString();
        }
        audioS = GetComponent<AudioSource>();
        foreach (ListSkin l in list)
        {
            if (l.Dimon == 0)
            {
                l.Bought = true;
            }
            else
            {
                l.Bought = PlayerPrefs.GetInt(l.name, 0) == 0 ? false : true;
            }
        }

        //if (!PlayerPrefs.HasKey("BOUGHT"))
        //{
        //    PlayerPrefs.SetInt("BOUGHT", 0);
        //    Load_bought();
        //}
        //else
        //{
        //    Load_bought();
        //}
        //Update_boughtSkin();
      //  UpdateUi();

    }
    private void OnDisable()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.acVideo_Donate -= Donated;
            Debug.Log("turn off donate");
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < change.Count; i++) // chon skin
        {
            if (n == i)
            {
                change[i].SetActive(true);

            }
            else
            {
                change[i].SetActive(false);

            }

        }
        UpdateUi();
        if (Input.GetKey(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
        }
    }
     
    public void Skin()
    {
        Panel_Skin.SetActive(true);
        //n = 0;
    }

    public void Close_skin()
    {
        Panel_Skin.SetActive(false);
       
    }
   
    public void Unlock_Premium()
    {
        check = false;
        Premium.SetActive(true);
        Rescue.SetActive(false);
        Premium_high.enabled = true;
        Rescue_high.enabled = false;
        Rescue_low.enabled = true;
        change_1.List_skin = 2;
        change_2.List_skin = 2;
        n = 0;
        audioS.clip = click;
        audioS.Play();
        switch (n)
        {
            default:
                BuySkin.interactable = true;
               // Tryskin.interactable = true;
                break;
        }
    }

    public void Unlock_Resuce()
    {
        check = true;
        Premium.SetActive(false);
        Rescue.SetActive(true);

        Rescue_high.enabled = true;
        Premium_high.enabled = false;
        Premium_low.enabled = true;

        change_1.List_skin = 17;
        change_2.List_skin = 17;

        audioS.clip = click;
        audioS.Play();

        n = 14;
        switch (n)
        {
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
                {
                    BuySkin.interactable = false;
                   // Tryskin.interactable = false;
                  
                    break;
                }

        }
    }

    public void Enter_skin(int a)
    {
        n = a;
        audioS.clip = click;
        audioS.Play();
        ListSkin l = list[n];
        if (l.Bought)
        {
            if (s != n)
            {
                selected = false;

            }
            if (s == n)
            {
                selected = true;
            }
        }
        else
        {
            return;
        }
        UpdateSelect();
    }
    private void Load()
    {
        n = PlayerPrefs.GetInt("selectOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectOption", n);
    }

    private void Load_bought()
    {
        ListSkin l = list[n];
        l.Bought = PlayerPrefs.GetInt("BOUGHT") == 1;
    }

    private void Save_bought()
    {
        ListSkin l = list[n];    
        PlayerPrefs.SetInt("BOUGHT",l.Bought ? 1 : 0);
    }
  
    public void Buy_skin()
    {
        ListSkin l = list[n];
        PlayerPrefs.SetInt(l.name, 1);
        l.Bought = true;
        int updateDimon = (PlayerPrefs.GetInt("SUMDIAMON", 0) - l.Dimon);
        Diamon_Money.text = updateDimon.ToString();
        PlayerPrefs.SetInt("SUMDIAMON", updateDimon);


    }
   
    private void Update_boughtSkin()
    {
        ListSkin l = list[n];

        if (l.Bought)
        {
            image_btn[n].image.enabled = true;
            image_lock[n].SetActive(false);
           // SelectSkin.gameObject.SetActive(true);
        }
        else
        {
            image_btn[n].image.enabled = false;
            image_lock[n].SetActive(true);
           
        }
    }
    public bool selected= false;

    private void UpdateUi()
    {
        ListSkin l = list[n];
        if (l.Bought)
        {
            BuySkin.gameObject.SetActive(false);
            Tryskin.gameObject.SetActive(false);
            SelectSkin.gameObject.SetActive(true);
            //image_btn[n].image.enabled = true;
            //image_lock[n].SetActive(false);


        }
        else
        {
            BuySkin.gameObject.SetActive(true);
            if (BuySkin.GetComponentInChildren<Text>().text.Length < 5)
            {
                BuySkin.GetComponentInChildren<Text>().text = "  " + l.Dimon;
            }
            if (BuySkin.GetComponentInChildren<Text>().text.Length >= 5)
            {
                BuySkin.GetComponentInChildren<Text>().text = " " + l.Dimon;
            }

            Tryskin.gameObject.SetActive(true);
            SelectSkin.gameObject.SetActive(false);
            SelectedSkin.gameObject.SetActive(false);
            //image_btn[n].image.enabled = false;
            //image_lock[n].SetActive(true);

            if (l.Dimon < PlayerPrefs.GetInt("SUMDIAMON", 0) && !check)
            {
                BuySkin.interactable = true;
               
            }
            else
            {
                BuySkin.interactable = false;
              
            }
        }
    }
    
    public int s;
    public void Select()
    {
        s = n;
        if (selected == false)
        {
            selected = true;
        }

        UpdateSelect();
        Save_selected();

    }
   private void UpdateSelect()
    {

        if (selected == false)
        {
            SelectSkin.gameObject.SetActive(true);
            SelectedSkin.gameObject.SetActive(false);
        }
        else
        {
            SelectSkin.gameObject.SetActive(false);
            SelectedSkin.gameObject.SetActive(true);
        }
    }
    private void Load_selected()
    {
        selected = PlayerPrefs.GetInt("SELECTED") == 1;
    }

    private void Save_selected()
    {
        PlayerPrefs.SetInt("SELECTED", selected ? 1 : 0);
    }

    public void Donate_Dianon()
    {
        AdsManager.Instance.ShowVideoReward();
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.acVideo_Donate += Donated;
        }
    }

    private void Donated()
    {
        int donate = (PlayerPrefs.GetInt("SUMDIAMON",0) + 100);
        Diamon_Money.text = donate.ToString();
        PlayerPrefs.SetInt("SUMDIAMON", donate);
        OnDisable();
    }

}


