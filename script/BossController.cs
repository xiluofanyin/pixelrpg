using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public string Bossname;
    public int boosHP;
    public int stage1, stage2 ;
    public GameObject boss, door;
    public Transform[] points;
    private Vector3 moveTarget;
    public float moveSpeed;
    public float timeact, timeperiod, firtdelay;
    private float actCounter, startCounter;
    public GameObject deathEffict;
    public BossShot theS;
    public Transform[] Shotpoints;
    public Transform shotCen;
    public float shottimePeriod,shotRotaSpeed,shotTime;
    private float shotCounter,shottimeCounter;
    public AudioSource levelBgm, bossBgm;
    public GameObject reward;
    public string progressMark;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveController.instance.CheckProgress(progressMark))
        {
            gameObject.SetActive(false);
        }
        else
        {
            door.SetActive(true);
            startCounter = firtdelay;
            UIController.Instance.bossSlider.maxValue = boosHP;
            UIController.Instance.bossSlider.value = boosHP;
            UIController.Instance.bossSlider.gameObject.SetActive(true);
            UIController.Instance.bossName.text = Bossname;
            UIController.Instance.bossName.gameObject.SetActive(true);
            levelBgm.Stop();
            bossBgm.Play();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (boosHP > 0)
        {
            if (startCounter > 0)
            {
                startCounter -= Time.deltaTime;
                if (startCounter < 0)
                {
                    actCounter = timeact;
                    shottimeCounter = shotTime;
                    boss.transform.position = points[Random.Range(0, points.Length)].position;
                    moveTarget = points[Random.Range(0, points.Length)].position;
                    while (moveTarget == boss.transform.position)
                    {
                        moveTarget = points[Random.Range(0, points.Length)].position;
                    }
                    boss.SetActive(true);
                }
            }
            else
            {
                actCounter -= Time.deltaTime;
                if (actCounter <= 0)
                {
                    startCounter = timeperiod;
                    boss.SetActive(false);
                }
                boss.transform.position = Vector3.MoveTowards(boss.transform.position, moveTarget, moveSpeed * Time.deltaTime);
                    if (shottimeCounter > 0)
                    {
                        shottimeCounter -= Time.deltaTime;

                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            shotCounter = shottimePeriod;
                            if (boosHP > stage1)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    Instantiate(theS, Shotpoints[i].position, Shotpoints[i].rotation).setdir(shotCen.position);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < Shotpoints.Length; i++)
                                {
                                    Instantiate(theS, Shotpoints[i].position, Shotpoints[i].rotation).setdir(shotCen.position);
                                }

                            }
                        }
                        if (boosHP < stage2)
                        {
                            transform.rotation = Quaternion.Euler(shotCen.transform.rotation.eulerAngles.x, shotCen.transform.rotation.eulerAngles.y, shotCen.transform.rotation.eulerAngles.z + (shotRotaSpeed * Time.deltaTime));
                        }
                    }
            }

        }
    }
    public void damge(int damagetake)
    {
        boosHP-=damagetake;
        if(boosHP<=0)
        {
            boosHP = 0;
            boss.SetActive(false);
            //door.SetActive(false);
            Instantiate(deathEffict,boss.transform.position,transform.rotation);
            UIController.Instance.bossSlider.gameObject.SetActive(false);
           
            UIController.Instance.bossName.gameObject.SetActive(false);
            levelBgm.Play();
            bossBgm.Stop();
            reward.SetActive(true);
            SaveController.instance.markProgress(progressMark);
        }
        UIController.Instance.bossSlider.value = boosHP;
    }
}
