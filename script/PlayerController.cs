using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    [HideInInspector]
    public Rigidbody2D theRB;
    private Animator anim;
    public SpriteRenderer theSR;
    public Sprite[] playerDirectionSprites;

    public Animator wpnAnim;
    private bool isKonckBack;
    public float knockbackTime, knockbackForce;
    private float knockbackCounter;
    private Vector2 knockDir;
    public GameObject hitEffect;
    public float dashSpeed, dashLength,dashcost;
    private float dashCounter,actmoveSpeed;
    public float totalStamina, staminupspeed;
    public  float currstamin;
    private bool isSpin;
    public float spincost, spincool;
    private float spinCount;
    public bool canmove;
    public SpriteRenderer swordSR;
    public Sprite[] AllSword;
    public damageEnemy swordDamage;
    public int curDamage;
    public Vector3 againPosition;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = SaveController.instance.save.startPosition;
        curDamage = SaveController.instance.save.curSword;
        swordSR.sprite=AllSword[curDamage];
        swordDamage.damage = SaveController.instance.save.swordDamage;
        totalStamina = SaveController.instance.save.sta;
        anim=GetComponent<Animator>();
        theRB=GetComponent<Rigidbody2D>();
        actmoveSpeed = moveSpeed;
        currstamin = totalStamina;
        UIController.Instance.upsta();
    }

    // Update is called once per frame
    void Update()
    {
        if (canmove&&!GameManager.instance.diaact)
        {
            if (!isKonckBack)
            {
                //transform.position=new Vector3(transform.position.x+(Input.GetAxisRaw("Horizontal")*moveSpeed*Time.deltaTime),transform.position.y+(Input.GetAxisRaw("Vertical")*moveSpeed*Time.deltaTime),transform.position.z);
                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * actmoveSpeed;

                anim.SetFloat("speed", theRB.velocity.magnitude);

                if (theRB.velocity != Vector2.zero)
                {
                    if (Input.GetAxisRaw("Horizontal") != 0)
                    {
                        theSR.sprite = playerDirectionSprites[1];

                        if (Input.GetAxisRaw("Horizontal") < 0)
                        {
                            theSR.flipX = true;
                            wpnAnim.SetFloat("dirX", -1f);
                            wpnAnim.SetFloat("dirY", 0f);
                        }
                        else
                        {
                            theSR.flipX = false;
                            wpnAnim.SetFloat("dirX", 1f);
                            wpnAnim.SetFloat("dirY", 0f);
                        }
                    }
                    else
                    {
                        if (Input.GetAxisRaw("Vertical") < 0)
                        {
                            theSR.sprite = playerDirectionSprites[0];
                            wpnAnim.SetFloat("dirX", 0f);
                            wpnAnim.SetFloat("dirY", -1f);
                        }
                        else
                        {
                            theSR.sprite = playerDirectionSprites[2];
                            wpnAnim.SetFloat("dirX", 0f);
                            wpnAnim.SetFloat("dirY", 1f);
                        }
                    }
                }

                if (Input.GetMouseButtonDown(0) && !isSpin)
                {
                    wpnAnim.SetTrigger("attack");
                    AudioController.instance.play(0);
                }
                if (dashCounter <= 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && currstamin >= dashcost)
                    {
                        actmoveSpeed = dashSpeed;
                        dashCounter = dashLength;
                        currstamin -= dashcost;
                    }
                }
                else
                {
                    dashCounter -= Time.deltaTime;
                    if (dashCounter <= 0)
                    {
                        actmoveSpeed = moveSpeed;
                    }
                }
                if (spinCount <= 0)
                {
                    if (Input.GetMouseButtonDown(1) && currstamin >= spincost)
                    {
                        wpnAnim.SetTrigger("Spinattack");
                        currstamin -= spincost;
                        spinCount = spincool;
                        isSpin = true;
                        AudioController.instance.play(0);
                    }


                }
                else
                {
                    spinCount -= Time.deltaTime;
                    if (spinCount <= 0)
                    {
                        isSpin = false;
                    }
                }
                currstamin += staminupspeed * Time.deltaTime;
                if (currstamin > totalStamina)
                {
                    currstamin = totalStamina;
                }
                Mathf.RoundToInt(currstamin);
                UIController.Instance.upsta();
            }
            else
            {
                knockbackCounter -= Time.deltaTime;
                theRB.velocity = knockDir * knockbackForce;
                if (knockbackCounter <= 0)
                {
                    isKonckBack = false;

                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
            anim.SetFloat("speed", 0f);
        }
    }
    public void konckback(Vector3 knockPosition)
    {
        knockbackCounter = knockbackTime;
        isKonckBack = true;
        knockDir = transform.position - knockPosition;
        knockDir.Normalize();
        Instantiate(hitEffect, transform.position, transform.rotation);
    }
    public void levelmoveing()
    {
        canmove = true;
        againPosition = transform.position; 
    }
    public void upGradeSword(int Damage,int newJudge)
    {
        swordDamage.damage = Damage;
        curDamage = newJudge;
        swordSR.sprite = AllSword[newJudge];
        SaveController.instance.save.curSword = curDamage;
        SaveController.instance.save.swordDamage = Damage;
    }
    public void Resetionagain()
    {
        transform.position = againPosition;
        canmove=false;
        gameObject.SetActive(true);
        currstamin = totalStamina;
        knockbackCounter = 0f;
        PlayerhealthController.instance.currentHealth = PlayerhealthController.instance.maxHealth;
    }
}
