using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D m_PlayerRigidbody;
    [SerializeField] private float m_SensetivityHor = 6.0f;
    [SerializeField] private float m_SensetivityVert = 8.0f;
    Animator m_CharacterAnimator;
    SpriteRenderer m_Sprite;
    [SerializeField] private bool m_IsGround = false; // находитьсяли персонаж на землеили впрыжке?
    [SerializeField] Transform m_GroundCheck; // ссылка на компонент Transform объекта для определения соприкосновения с землей.
    [SerializeField] private float m_GroundRadius = 0.3f; // радиус определения соприкосновения с землей.
    [SerializeField] LayerMask m_WhatIsGround; // ссылка на слой, представляющий землю.
    bool walkActivate;
    [SerializeField] Button jump;
    int direction = 1;
    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        m_CharacterAnimator = GetComponent<Animator>();
        m_Sprite = gameObject.GetComponent<SpriteRenderer>();
        m_PlayerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetButton("Horizontal") || walkActivate)
        {
            PlayerMove(); 
            m_CharacterAnimator.SetInteger("Pose", 1);
        }
        else if (Input.GetButton("Horizontal") && Input.GetKey(KeyCode.Space))
        {
           
            m_PlayerRigidbody.AddForce(new Vector2(0, 600));
            m_CharacterAnimator.SetInteger("Pose", 2);
        }
        else 
        {
            m_CharacterAnimator.SetInteger("Pose", 0); 
        }

        // если персонаж на земле и нажат пробел
        if (m_IsGround && Input.GetKeyDown(KeyCode.Space))
        {
            // устанавливаем в аниматоре переменную false
         
            m_PlayerRigidbody.AddForce(new Vector2(0, 600));
            m_CharacterAnimator.SetInteger("Pose", 2);
        }
  
    }
    public void WalkFrontMethod()
    {
        walkActivate = true;
        direction = 1;
    }

    public void WalkBackMethod()
    {
        walkActivate = true;
        direction = -1;
    }

    public void StopMethod()
    {
        walkActivate = false;
    }
    void FixedUpdate()
    {
        // определяем на землели персонаж
        m_IsGround = Physics2D.OverlapCircle(m_GroundCheck.position,
            m_GroundRadius, m_WhatIsGround);
        // устанавливаем соответствующую переменную в аниматоре
        m_CharacterAnimator.SetBool("Ground", m_IsGround);
        // если игрок в прыжке то делаем выход из метода, чтобы не выполнялисьдействия связанные с бегом.
        if (!m_IsGround)
            return;

    }


    void PlayerMove()
    {
        Vector3 tempvector = Vector3.right*direction;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, m_SensetivityHor * Time.deltaTime);
        if (tempvector.x < 0)
        {
            m_Sprite.flipX = true;
        }
        else
        {
            m_Sprite.flipX = false;
        }
    } 
  
    public void Jump()
    {  
        m_PlayerRigidbody.AddForce(transform.up * m_SensetivityVert, ForceMode2D.Impulse);  
    }

    void CheckGround()
    {

        if (m_IsGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            m_CharacterAnimator.SetInteger("Pose", 2);
        }
       
    }
}
