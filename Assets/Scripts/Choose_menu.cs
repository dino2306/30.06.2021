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
        ListSkin l = list[n];
        l.Number = PlayerPrefs.GetInt("NUMBER", 0);

        Diamon_Money.text = AdsManager.Instance.Sum_diamon.ToString();

        audioS = GetComponent<AudioSource>();
        //if (!PlayerPrefs.HasKey("SELECTED"))
        //{
        //    PlayerPrefs.SetInt("SELECTED", 0);
        //    Load_selected();
        //}
        //else
        //{
        //    Load_selected();
        //}
        //UpdateSelect();


        if (!PlayerPrefs.HasKey("BOUGHT"))
        {
            PlayerPrefs.SetInt("BOUGHT", 0);
            Load_bought();
        }
        else
        {
            Load_bought();
        }
        Update_boughtSkin();
      //  UpdateUi();

    }
    private void OnDisable()
    {

        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.acVideo_buy -= Bought_skin;
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
        AdsManager.Instance.ShowVideoReward();

        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.acVideo_buy += Bought_skin;
            AdsManager.Instance.acTryVideo = null;
        }
      
    }
    private void Bought_skin()
    {
        ListSkin l = list[n];
        l.Number ++;

        if (l.Number == 5)
        {
            if (l.Bought == false)
            {
                l.Bought = true;
            }
            Update_boughtSkin();
        }
        AdsManager.Instance.acVideo_buy -= Bought_skin;
        PlayerPrefs.SetInt("NUMBER", l.Number);
        Save_bought();

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
        PlayerPrefs.SetInt(l.name, 1);
      

    }
    public bool selected= false;

    private void UpdateUi()
    {
        ListSkin l = list[n];
        
        {
            if (l.Bought)
            {
                BuySkin.gameObject.SetActive(false);
                Tryskin.gameObject.SetActive(false);
                SelectSkin.gameObject.SetActive(true);
                image_btn[n].image.enabled = true;
                image_lock[n].SetActive(false);


            }
            else
            {
                BuySkin.gameObject.SetActive(true);
                Tryskin.gameObject.SetActive(true);
                SelectSkin.gameObject.SetActive(false);
                SelectedSkin.gameObject.SetActive(false);
                image_btn[n].image.enabled = false;
                image_lock[n].SetActive(true);
             //   BuySkin.GetComponentInChildren<Text>().text = l.Number + "/";
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
        ListSkin l = list[n];
       UpdateSelect();
        Save_selected();

    }
    public void UpdateSelect()
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

}


