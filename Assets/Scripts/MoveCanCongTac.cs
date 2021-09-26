using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanCongTac : MonoBehaviour
{
    public LayerMask lmPlayer;
    //  public Transform leftPoint, rightPoint;
    public Transform _trParent;
    public bool isLeft;

    public Transform TamvanTim;
    public Vector3 Vstar, Vend;
    public float speed;

    private AudioSource audioS;
    public AudioClip cancongtac;
   // public float speedRotate;

  //  Vector3 alo = new Vector3(0, 0, 40);
    // Start is called before the first frame update
    void Start()
    {
        Vstar = TamvanTim.position;
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.05f, lmPlayer))
        {
            if (isLeft)
            {
                audioS.clip = cancongtac;
                audioS.Play();

                _trParent.eulerAngles = new Vector3(0, 0, -40);

                // _trParent.Rotate(new Vector3(0, 0, -40) * Time.deltaTime * speedRotate);
                //  TamvanTim.position = Vector3.MoveTowards(TamvanTim.position, Vend, Time.deltaTime * speed);

            }
            else
            {
                audioS.clip = cancongtac;
                audioS.Play();
                _trParent.eulerAngles = new Vector3(0, 0, 40);

                //  TamvanTim.position = Vector3.MoveTowards(TamvanTim.position, Vstar, Time.deltaTime * speed);
            }
        }
        if (_trParent.rotation.z > 0)
        {
            TamvanTim.position = Vector3.MoveTowards(TamvanTim.position, Vend, Time.deltaTime * speed);
        }
        else
        {

            TamvanTim.position = Vector3.MoveTowards(TamvanTim.position, Vstar, Time.deltaTime * speed);
        }

    }


}
