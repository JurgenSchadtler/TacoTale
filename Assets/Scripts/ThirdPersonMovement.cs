using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Animator anim;
    private AudioSource mAudioSrc;
    public GameObject freeCam, battleCam, battleDisplay, PlayerHPText, EnemyHPText;
    public GameObject playerPos, enemyPos, spawn;
    private bool inBattle = false, ready = true;
    private int currentAP = 4;
    private int tacos = 0;
    private GameObject enemy;
    private Vector3 enemyPosi;
    public CanvasActionPoints CanvasActionPoints;
    public TacoPoints tacoPoints;
    private int aux, enemyHP, playerHP = 100;
    private bool finish = true;
    public string dead,win;

    private Vector3 currentPos, currentRot;

    void Start()
    {
        anim = GetComponent<Animator>();
        mAudioSrc = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        if (!inBattle)
        {
            
            battleDisplay.SetActive(false);
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


            if (direction.magnitude >= 0.1f)
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
                anim.SetInteger("state", 1);
            }
            else
            {
                anim.SetInteger("state", 0);
            }

            if(tacos == 5)
            {
                SceneManager.LoadScene(win);
            }
        }

        else if(finish == true)
        {
            if (enemyHP <= 0 )
            {
                inBattle = false;
                battleCam.SetActive(false);
                freeCam.SetActive(true);
                transform.position = currentPos;
                Destroy(enemy);


            }

            else if (currentAP == 0 && ready == true)
            {
                ready = false;
                StartCoroutine(enemyTurn());

            }

            if(playerHP <= 0)
            {
                SceneManager.LoadScene(dead);
            }
            
            //Combat

            if (Input.GetKeyDown(KeyCode.J) && currentAP >= 3)
            {
                mAudioSrc.Play();
                transform.Rotate(0, 20, 0);
                anim.SetInteger("state", 2);
                currentAP -= 3;
                CanvasActionPoints.setpoints(currentAP);
                aux = GetRandomNum();
                if(aux >= 50)
                {
                    enemy.tag = "Hit";
                   
                    enemyHP -= 65;
                    
                }
                finish = false;
                
                StartCoroutine(delay());
            }

            if (Input.GetKeyDown(KeyCode.K) && currentAP >= 1 )
            {
                mAudioSrc.Play();
                anim.SetInteger("state", 4);
                currentAP -= 1;
                CanvasActionPoints.setpoints(currentAP);
                aux = GetRandomNum();
                if (aux >= 25)
                {
                    enemy.tag = "Hit";
                    
                    enemyHP -= 15;
                    Debug.Log("quisho");
                    
                }
                finish = false;
                StartCoroutine(delay());
            }

            if (Input.GetKeyDown(KeyCode.L) && currentAP >= 2)
            {
                mAudioSrc.Play();
                anim.SetInteger("state", 3);
                currentAP -= 2;
                CanvasActionPoints.setpoints(currentAP);
                aux = GetRandomNum();
                if (aux >= 10)
                {
                    enemy.tag = "Hit";
                    
                    enemyHP -= 35;
                    Debug.Log("sho");
                }
                finish = false;
                StartCoroutine(delay());
            }

            if (Input.GetKeyDown(KeyCode.Y) && currentAP >= 2)
            {
                
                currentAP -= 2;
                CanvasActionPoints.setpoints(currentAP);
                aux = GetRandomNum();
                if (aux >= 25)
                {
                    finish = false;
                    anim.SetInteger("state", 6);
                    //Corrutina para flee
                    StartCoroutine(delay());
                    enemy.transform.position = enemyPosi;
                    enemy.tag = "Enemy";
                }
                
                

            }

            //Enemy Turn

            

            PlayerHPText.GetComponent<UnityEngine.UI.Text>().text = playerHP + "/100";
            EnemyHPText.GetComponent<UnityEngine.UI.Text>().text = enemyHP + "/100";

            

           
        }


        /*Test de Battle
        if(Input.GetKeyDown(KeyCode.T))
        {
            currentPos = transform.position;
            
            enemyHP = 100;
            currentAP = 4;
            CanvasActionPoints.setpoints(currentAP);
            inBattle = true;
            freeCam.SetActive(false);

            battleCam.SetActive(true);
            battleDisplay.SetActive(true);


            transform.position = playerPos.transform.position;
            transform.rotation = playerPos.transform.rotation;
            
            enemy.transform.position = enemyPos.transform.position;
            //enemy.transform.rotation = enemyPos.transform.rotation;


        }*/

        

    }

    public int GetRandomNum()
    {
        return Random.Range(0, 100);
    }

    IEnumerator enemyTurn()
    {
        yield return new WaitForSeconds(2);
        enemy.tag = "Attack";
        yield return new WaitForSeconds(1);
        aux = GetRandomNum();

        
        if(aux >= 35)
        {
            anim.SetInteger("state", 5);
            playerHP -= 45;
            
        }
        yield return new WaitForSeconds(2);
        anim.SetInteger("state", 0);
        yield return new WaitForSeconds(2);

        
        if (aux >= 50)
        {
            anim.SetInteger("state", 5);
            playerHP -= 10;
            
        }
        yield return new WaitForSeconds(2);
        Debug.Log("turno2");
        anim.SetInteger("state", 0);
        yield return new WaitForSeconds(2);

       
        if (aux >= 25)
        {
            anim.SetInteger("state", 5);
            playerHP -= 15;
            
        }
        yield return new WaitForSeconds(2);
        Debug.Log("turno3");
        anim.SetInteger("state", 0);
        enemy.tag = "Enemy";
        yield return new WaitForSeconds(2);

        
        ready = true;
        currentAP = 4;
        CanvasActionPoints.setpoints(currentAP);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2);
        finish = true;
        
        if (anim.GetInteger("state") == 2)
        {
            transform.Rotate(0, -20, 0);
        }

        if(anim.GetInteger("state") == 6)
        {

            inBattle = false;
            battleCam.SetActive(false);
            freeCam.SetActive(true);
            transform.position = spawn.transform.position;
            Debug.Log("Pussy");

        }

        anim.SetInteger("state", 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Enemy")
        {
            anim.SetInteger("state", 0);
            enemy = other.gameObject;
            currentAP = 4;
            currentPos = transform.position;

            enemyHP = 100;
            CanvasActionPoints.setpoints(currentAP);
            inBattle = true;
            freeCam.SetActive(false);

            battleCam.SetActive(true);
            battleDisplay.SetActive(true);


            transform.position = playerPos.transform.position;
            transform.rotation = playerPos.transform.rotation;

            enemyPosi = enemy.transform.position;
            enemy.transform.position = enemyPos.transform.position;
            //enemy.transform.rotation = enemyPos.transform.rotation;
        }

        if(other.tag == "Taco")
        {
            if(playerHP <= 100)
            {
                playerHP += 50;
            }

            Debug.Log("TacoPoint");
            tacos++;
            Debug.Log(tacos);
            tacoPoints.setTacos(tacos);
        }
    }
}
