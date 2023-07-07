using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed; // �ӵ�
    [SerializeField]
    private float maxSpeed; // �ִ� �ӵ�
  
    [SerializeField]
    int maxForce;
    [SerializeField]
    int maxJumpCount = 1; // �ִ� ���� Ƚ��

    [SerializeField]
    private bool isGround = false; // ���� ��� �ִ°�?
    [SerializeField]
    private bool isReadyToJump = false; // ������ �غ��ϰ� �ִ°�?
    private float curTime = 0f;
    private float inputTime = 0f;
    private LayerMask layerMask;
    [SerializeField]
    private int curJumpCount = 0;
    [SerializeField]
    private UIcontroller UIbarObj;


    // �ʿ��� ������Ʈ
    private Rigidbody2D rigid;
    private CapsuleCollider2D capsuleCol2D;
    [SerializeField]
    private Camera cam;
    private Animator anim;

    public SoundManager soundmanager;
    // �ʿ��� ����;
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
        // �ڽ� ���·� �浹�� �ݶ��̴� ����
        if (Physics2D.BoxCast(capsuleCol2D.bounds.center,   // Vector2 origin    -> ��������
            capsuleCol2D.bounds.extents,                       // Vector2 size      -> �ڽ��� ũ��
            0f,                                             // float   angle     -> �ڽ��� ����
            Vector2.down,                                   // vector2 direction -> �ڽ��� ����
            1f,                                           // float   distance  -> �ڽ��� �ִ� �Ÿ�
            layerMask = LayerMask.GetMask("NormalField")))
        {
            if (rigid.velocity.y <= 0) // �������� ���� ��
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
        float addJF; // �����ϴ� ��
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
