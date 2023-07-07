using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed; // 속도
    [SerializeField]
    private float maxSpeed; // 최대 속도
  
    [SerializeField]
    int maxForce;
    [SerializeField]
    int maxJumpCount = 1; // 최대 점프 횟수

    [SerializeField]
    private bool isGround = false; // 땅에 닿아 있는가?
    [SerializeField]
    private bool isReadyToJump = false; // 점프를 준비하고 있는가?
    private float curTime = 0f;
    private float inputTime = 0f;
    private LayerMask layerMask;
    [SerializeField]
    private int curJumpCount = 0;
    [SerializeField]
    private UIcontroller UIbarObj;


    // 필요한 컴포넌트
    private Rigidbody2D rigid;
    private CapsuleCollider2D capsuleCol2D;
    [SerializeField]
    private Camera cam;
    private Animator anim;

    public SoundManager soundmanager;
    // 필요한 사운드;
    public string BGM;

    public float lookDirX = 1f;

    private void Awake()
    {
        soundmanager.BGM.clip.Play();
        rigid = GetComponent<Rigidbody2D>();
        capsuleCol2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(this.transform.localEulerAngles.y == 0f)
        {
            lookDirX = -1f;
        }
        else if(this.transform.localEulerAngles.y == 180f)
        {         
            lookDirX = 1f;
        }
        IsStep();
        Move();
        TryJump();
        //Timer();
    }

    private void IsStep()
    {
        // 박스 형태로 충돌한 콜라이더 감지
        if (Physics2D.BoxCast(capsuleCol2D.bounds.center,   // Vector2 origin    -> 시작지점
            capsuleCol2D.bounds.extents,                       // Vector2 size      -> 박스의 크기
            0f,                                             // float   angle     -> 박스의 각도
            Vector2.down,                                   // vector2 direction -> 박스의 방향
            1f,                                           // float   distance  -> 박스의 최대 거리
            layerMask = LayerMask.GetMask("NormalField")))
        {
            if (rigid.velocity.y <= 0) // 떨어지고 있을 때
            {
                isGround = true;
                curJumpCount = 0;
                anim.SetBool("isJump", false);
            }
            else
            {
                isGround = false;
                isReadyToJump = false;
            }
        }
        else
        {
            isGround = false;
        }
        
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        if ((moveDirX == 0f) || isReadyToJump)
        {
            return;
        }
        if (moveDirX < 0)
        {
            this.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (moveDirX > 0)
        {
            this.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void TryJump()
    {
        if (!isGround || curJumpCount >= maxJumpCount)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isReadyToJump = true;
            inputTime = 0;
            UIbarObj.posX = 0;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            inputTime += Time.deltaTime;
            UIbarObj.posX = Mathf.Sin(inputTime * UIbarObj.A) * UIbarObj.maxsize;
            anim.SetBool("isReadToJump", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {              
            if (isGround)
            {
                Jump(inputTime);
                anim.SetBool("isReadToJump", false);
                inputTime = 0;                
            }
            
        }
    }

    private void Jump(float inputTime)
    {
        curJumpCount++;
        anim.SetBool("isJump", true);
        float addJF; // 증가하는 힘
        addJF = (Mathf.Sin(inputTime * UIbarObj.A) * maxForce) + maxForce + 1;       
        rigid.velocity = new Vector2(rigid.velocity.x + (lookDirX * addJF * 0.6f), addJF);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "JumpStage")
        {
            if (rigid.velocity.y >= 0)
            {
                cam.transform.position = new Vector3(cam.transform.position.x, 78f, cam.transform.position.z);
            }
            if(rigid.velocity.y < 0) 
            {
                cam.transform.position = new Vector3(cam.transform.position.x, 34f, cam.transform.position.z);
            }
        }
        else if(collision.tag == "Exit")
        {
            SceneManager.LoadScene("ending");
        }
    }

}
