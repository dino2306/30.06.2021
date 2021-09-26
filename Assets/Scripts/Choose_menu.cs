using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    // Start is called before the first frame update
    void Start()
    {     
        audioS = GetComponent<AudioSource>();
        //if (!PlayerPrefs.HasKey("selectOption"))
        //{
        //    PlayerPrefs.SetInt("selectOption", 1);
        //    Load();
        //}
        //else
        //{
        //    Load();
        //}
        
    
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
        //UpdateUi();
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
        n = 0;
    }

    public void Close_skin()
    {
        Panel_Skin.SetActive(false);
    }

    public void Unlock_Premium()
    {
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
    }

    public void Unlock_Resuce()
    {
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
    }

    public void Enter_skin(int a)
    {
        n = a;
        audioS.clip = click;
        audioS.Play();
        if (s != n)
        {
            selected = false;

        }
        if (s == n)
        {
            selected = true;
        }

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
        AdsManager.Instance.ShowRewarded((a) =>
        {
            if (a)
            {
                ListSkin l = list[n];

                if (l.Bought == false)
                {
                    l.Bought = true;
                }
              
                Update_boughtSkin();
                // Save();
                PlayerPrefs.GetInt("buy", 1);
                Save_bought();

            }
        });
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
            //  SelectSkin.gameObject.SetActive(false);
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

   
}


