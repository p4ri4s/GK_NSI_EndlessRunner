using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public GameObject[] models;
    private static Animator anim;
    private Vector3 moveVector;
    private float verticalVelocity;
    private float gravity = 0.7f;
    private float speed = 5.0f;
    private float jumpForce = 0.2f;
    private float introAnimTime = 8.27f;
    public GameObject jumpSideEffect;
    public GameObject deathEffect;
    public GameObject scoreObject;
    private float score = 0.0f;
    public Text scoreText;
    private float speedIncrease = 0.02f;
    private bool canJumpSide = true;
    private bool canJumpUp = true;
    public GameObject gameOverPanel;
    public Text gameOverScore;
    public GameObject newRecordPanel;
    private bool isDead = false;
    private float percentageIncreasing = 0.0f;
    private float scoreTimer = 0.0f;
    private bool changeControl = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        scoreObject.SetActive(false);
        getAnimator();
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad > introAnimTime)
        {
            moveVector = Vector3.zero;

            if(!isDead)
            {
                scoreObject.SetActive(true);
            }

            //Score 
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 0.3f)
            {
                score += 1 + (0.01f * transform.position.z) + ((1 + (0.01f * transform.position.z)) * percentageIncreasing);
                if(score < 0)
                {
                    score = 0;
                    scoreText.text = ((int)score).ToString();
                }
                else
                {
                    scoreText.text = ((int)score).ToString();
                }
                scoreTimer -= 0.3f;
            }
            speed += speedIncrease * Time.smoothDeltaTime;


            //Jump controller
            if (controller.isGrounded)
            {
                verticalVelocity = -gravity * Time.deltaTime;
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && canJumpUp && !isDead)
                {
                    verticalVelocity = jumpForce;
                    anim.SetTrigger("isJumping");
                    canJumpUp = false;
                    StartCoroutine(timeBetweenJumpUp());
                }
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }

            //  Left/Right move
            if(!changeControl)
            {
                if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a")) && transform.position.x >= 0 && controller.isGrounded && canJumpSide && !isDead)
                {
                    jumpSideEffect.transform.position = transform.position;
                    jumpSideEffect.GetComponent<ParticleSystem>().Play();
                    moveVector.x = moveVector.x - 2;
                    canJumpSide = false;
                    StartCoroutine(timeBetweenJumpSide());
                }

                if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d")) && transform.position.x <= 0 && controller.isGrounded && canJumpSide && !isDead)
                {
                    jumpSideEffect.transform.position = transform.position;
                    jumpSideEffect.GetComponent<ParticleSystem>().Play();
                    moveVector.x = moveVector.x + 2;
                    canJumpSide = false;
                    StartCoroutine(timeBetweenJumpSide());
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d")) && transform.position.x >= 0 && controller.isGrounded && canJumpSide && !isDead)
                {
                    jumpSideEffect.transform.position = transform.position;
                    jumpSideEffect.GetComponent<ParticleSystem>().Play();
                    moveVector.x = moveVector.x - 2;
                    canJumpSide = false;
                    StartCoroutine(timeBetweenJumpSide());
                }

                if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a")) && transform.position.x <= 0 && controller.isGrounded && canJumpSide && !isDead)
                {
                    jumpSideEffect.transform.position = transform.position;
                    jumpSideEffect.GetComponent<ParticleSystem>().Play();
                    moveVector.x = moveVector.x + 2;
                    canJumpSide = false;
                    StartCoroutine(timeBetweenJumpSide());
                }
            }
            

            // Move Forward
            moveVector.z = speed * Time.smoothDeltaTime;

            //  Up/Down move
            moveVector.y = verticalVelocity;

            //Move Player
            controller.Move(moveVector);
        }
    }
    
    // Get animator for chosen character 
    private void getAnimator()
    {
        models = GameObject.FindGameObjectsWithTag("Model");
        foreach(GameObject gO in models)
        {
            if(gO.activeInHierarchy)
            {
                anim = gO.GetComponent<Animator>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            isDead = true;
            if (PlayerPrefs.GetInt("HighScore") < (int)score)
            {
                newRecordPanel.SetActive(true);
                PlayerPrefs.SetFloat("HighScoreRoadLength", transform.position.z);
                PlayerPrefs.SetInt("HighScore", (int)score);
            }
            deathEffect.transform.position = transform.position;
            deathEffect.GetComponent<ParticleSystem>().Play();
            anim.SetTrigger("isDead");
            speed = 0;
            speedIncrease = 0;
            scoreObject.SetActive(false);
            gameOverScore.text = scoreText.text;
            gameOverPanel.SetActive(true);
        }
        if (other.tag == "changeControlObstacle")
        {
            StartCoroutine(timerChangeControl());
        }
        if (other.tag == "+10%_10sec")
        {
            StartCoroutine(timerChangePointIncrease(10, 0.1f));
        }
        if (other.tag == "+10%_20sec")
        {
            StartCoroutine(timerChangePointIncrease(20, 0.1f));
        }
        if (other.tag == "+20%_10sec")
        {
            StartCoroutine(timerChangePointIncrease(10, 0.2f));
        }
        if (other.tag == "+20%_20sec")
        {
            StartCoroutine(timerChangePointIncrease(20, 0.2f));
        }
        if (other.tag == "+50%_10sec")
        {
            StartCoroutine(timerChangePointIncrease(10, 0.5f));
        }
        if (other.tag == "+50%_20sec")
        {
            StartCoroutine(timerChangePointIncrease(20, 0.5f));
        }
        if (other.tag == "+100Coins")
        {
            score += 100;
        }
        if (other.tag == "+200Coins")
        {
            score += 200;
        }
        if (other.tag == "+500Coins")
        {
            score += 500;
        }
        if (other.tag == "-100Coins")
        {
            score -= 100;
        }
        if (other.tag == "-200Coins")
        {
            score -= 200;
        }
        if (other.tag == "-500Coins")
        {
            score -= 500;
        }
        if(other.tag == "-50%_10sec")
        {
            StartCoroutine(timerChangePointIncrease(10, -0.5f));
        }
    }

    IEnumerator timeBetweenJumpSide()
    {
        yield return new WaitForSeconds(0.1f);
        canJumpSide = true;
    }
    IEnumerator timeBetweenJumpUp()
    {
        yield return new WaitForSeconds(0.3f);
        canJumpUp = true;
    }

    IEnumerator timerChangePointIncrease(int time, float percent)
    {
        percentageIncreasing += percent;
        yield return new WaitForSeconds(time);
        percentageIncreasing -= percent;
    }

    IEnumerator timerChangeControl()
    {
        changeControl = true;
        yield return new WaitForSeconds(10f);
        changeControl = false;
    }
}
