using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public Animator anim;
    public BoxCollider2D area;
    public float moveSpeed;

    public float waitTime,moveTime;
    private float waitCounter,moveCounter;
    private Vector2 moveDir;
    public bool shouldChase;
    private bool isChase;
    public float chaseSpeed, rangeToChase,waitAfterHitting;
    public int damagetoplayer = 10;

    private bool isKonckBack;
    public float knockbackTime, knockbackForce,waitAfterKnock;
    private float knockbackCounter,knockWaitCounter;
    private Vector2 knockDir;
    public bool shouldshoot;
    public GameObject bullet;
    public float timeshootperiod;
    private float shootCounter;
    public Transform shootPosition;
    // Start is called before the first frame update
    void Start()
    {
        waitCounter = Random.Range(waitTime*0.75f,waitTime*1.25f);
        shootCounter = timeshootperiod;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKonckBack)
        {
            if (!isChase)
            {
                if (waitCounter > 0)
                {
                    waitCounter = waitCounter - Time.deltaTime;
                    theRB.velocity = Vector2.zero;
                    if (waitCounter <= 0)
                    {
                        moveCounter = Random.Range(moveTime * 0.75f, moveTime * 1.25f);
                        anim.SetBool("moving", true);

                        moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                        moveDir.Normalize();
                    }
                }
                else
                {
                    moveCounter -= Time.deltaTime;
                    theRB.velocity = moveDir * moveSpeed;
                    if (moveCounter <= 0)
                    {
                        waitCounter = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
                        anim.SetBool("moving", false);
                    }

                    if (shouldChase && PlayerhealthController.instance.gameObject.activeInHierarchy)
                    {
                        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChase)
                        {
                            isChase = true;
                        }
                    }
                }
                if(shouldshoot)
                {
                    shootCounter-= Time.deltaTime;  
                    if(shootCounter<=0)
                    {
                        shootCounter = timeshootperiod;
                        Instantiate(bullet,shootPosition.position,shootPosition.rotation);

                    }
                }
            }
            else
            {
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                    theRB.velocity = Vector2.zero;
                    if (waitCounter <= 0)
                    {
                        anim.SetBool("moving", true);
                    }
                }
                else
                {


                    moveDir = PlayerController.instance.transform.position - transform.position;
                    moveDir.Normalize();
                    theRB.velocity = moveDir * chaseSpeed;
                }
                if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > rangeToChase || !PlayerhealthController.instance.gameObject.activeInHierarchy)
                {
                    isChase = false;
                    waitCounter = waitTime;
                    anim.SetBool("moving", false);
                }
            }
        }
        else
        {
            if(knockbackCounter>0)
            {
                knockbackCounter -= Time.deltaTime;
                theRB.velocity = knockDir * knockbackForce;
                if(knockbackCounter <= 0)
                {
                    knockWaitCounter = waitAfterKnock;
                }
            }
            else
            {
                knockWaitCounter -= Time.deltaTime;
                theRB.velocity = Vector2.zero;
                if(knockWaitCounter <= 0)
                {
                    isKonckBack = false;
                }
            }
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, area.bounds.min.x + 1f, area.bounds.max.x - 1f), Mathf.Clamp(transform.position.y, area.bounds.min.y + 1f, area.bounds.max.y - 1f), transform.position.z);
    }

    public void knockback(Vector3 knockposition)
    {
        knockbackCounter = knockbackTime;
        isKonckBack = true;
        knockDir = transform.position - knockposition;
        knockDir.Normalize();
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            if(isChase)
            {
                waitCounter = waitAfterHitting;
                anim.SetBool("moving", false);

                PlayerController.instance.konckback(transform.position);
                PlayerhealthController.instance.damagePlayer(damagetoplayer);
            }
        }
    }
}
