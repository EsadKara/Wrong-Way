using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] GameObject spline, backpack, colliderObj;

    public float moveSpeed, rocketTime, rocketSpeed;
    public bool isGround, isSlide, doubleJump, hasRocket;

    int movementCount;
    float touchPos, jumpCount;

    Rigidbody playerRb;
    Animator playerAnim;
    gameManager gM;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();

        jumpCount = 0;
        moveSpeed = 1.5f;
        movementCount = -1;
        rocketTime = 10;

        hasRocket = false;
        isGround = true;
    }

    private void Update()
    {
        Move();
        SetAnimations();

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround && !isSlide) 
            {
                playerRb.AddForce(Vector3.up * jumpForce);
                jumpCount++;
            }
            else if (!isGround && jumpCount < 1 && gM.hasBoots) 
            {
                doubleJump = true;
                playerRb.AddForce(Vector3.up * jumpForce);
                jumpCount++;
            }
        }
        if (isGround)
        {
            doubleJump = false;
            jumpCount = 0;
        }

        //Slide
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isGround)
            {
                isSlide = true;
                Invoke("SlideFix", 0.3f);
            }
        }

        if (moveSpeed > 5)
        {
            moveSpeed = 5;
        }

        
        if (hasRocket)
        {
            isSlide = false;
            colliderObj.GetComponent<CapsuleCollider>().isTrigger = true;
            backpack.SetActive(true);
            playerRb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
            Quaternion newRot = Quaternion.Euler(60, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 5f * Time.deltaTime);
            rocketSpeed = 8;
            rocketTime -= Time.deltaTime;
            if (rocketTime <= 0)
            {
                hasRocket = false;
                rocketTime = 10;
            }
        }
        else
        {
            colliderObj.GetComponent<CapsuleCollider>().isTrigger = false;
            backpack.SetActive(false);
            playerRb.useGravity = true;
            Quaternion newRot = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 5f * Time.deltaTime);
        }
    }

    void SetAnimations()
    {
        playerAnim.SetBool("hasRocket", hasRocket);
        playerAnim.SetBool("isGround", isGround);
        playerAnim.SetBool("isSlide", isSlide);
        playerAnim.SetBool("doubleJump", doubleJump);
    }

   
    void Move()
    {
        if (hasRocket)
            transform.Translate(0, 0, rocketSpeed * Time.deltaTime);
        else
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);

        //Keyboard Control
        if (Input.GetKeyDown(KeyCode.D))
        {
            MovementCountIncrease();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MovementCountDecrease();
        }

        // Touch Controls

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPos = 50;
            }
            if (touch.deltaPosition.x > touchPos)
            {
                MovementCountIncrease();
                touchPos = Mathf.Infinity;
            }
            else if (touch.deltaPosition.x < -touchPos)
            {
                MovementCountDecrease();
                touchPos = Mathf.Infinity;
            }
            else if (touch.deltaPosition.y > touchPos)
            {
                if (isGround && !isSlide)
                {
                    playerRb.AddForce(Vector3.up * jumpForce);
                    jumpCount++;
                    touchPos = Mathf.Infinity;
                }
                else if (!isGround && jumpCount < 1 && gM.hasBoots)
                {
                    doubleJump = true;
                    playerRb.AddForce(Vector3.up * jumpForce);
                    jumpCount++;
                    touchPos = Mathf.Infinity;
                }
            }
            else if (touch.deltaPosition.y < -touchPos)
            {
                if (isGround)
                {
                    isSlide = true;
                    Invoke("SlideFix", 0.3f);
                    touchPos = Mathf.Infinity;
                }
            }
        }

        if (movementCount == 0)
        {
            transform.position = (Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 0), 5f * Time.deltaTime));
        }
        else if (movementCount == 1)
        {
            transform.position = (Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, -0.625f), 5f * Time.deltaTime));
        }
        else if (movementCount == -1)
        {
            transform.position = (Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 0.625f), 5f * Time.deltaTime));
        }
    }
    void SlideFix()
    {
        isSlide = false;
    }
    void MovementCountIncrease()
    {
        movementCount++;
        if (movementCount > 1)
        {
            movementCount = 1;
        }
    }
    void MovementCountDecrease()
    {
        movementCount--;
        if (movementCount < -1)
        {
            movementCount = -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if(!hasRocket)
                gM.GameOver();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (!hasRocket)
                gM.GameOver();
        }
    }


}
