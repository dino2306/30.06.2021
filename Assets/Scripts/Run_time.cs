using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Run_time : MonoBehaviour
{
    public float current_time;
    public Text Seconds, minutes;
    public int late ;

    public GameObject gameOver;

    private bool Over_Time = false;
    private void Start()
    {
       Seconds.text = current_time.ToString();
        minutes.text = "0" + late.ToString() + ":";
    }

    // Update is called once per frame
    void Update()
    {
        if (!Over_Time)
        {
            current_time -= Time.deltaTime;
            if (current_time < 10)
            {


                Seconds.text = "0" + current_time.ToString();

            }
            else
            {
                Seconds.text = current_time.ToString();
            }


            if (current_time < 0.1f)
            {
                late--;
                minutes.text = "0" + late.ToString() + ":";
                current_time = 60;
            }
        }
        EndTime();
    }

    void EndTime(){
        if (late == 0 && current_time < 1  )
        {
            Over_Time = true;
            gameOver.SetActive(true);
        }
    }
}
