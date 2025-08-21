using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public Vector2 inputVec;
    public Scaner scaner; 
    Rigidbody2D rg;
    SpriteRenderer sr;
    Animator animator;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scaner = GetComponent<Scaner>();
    }
    void Start()
    {
             
    }

    void FixedUpdate()
    {
        Vector2 nextvc = inputVec * MoveSpeed * Time.fixedDeltaTime;
        rg.MovePosition(rg.position + nextvc);
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            sr.flipX = inputVec.x < 0 ? true : false;
        }
        animator.SetFloat("Speed", inputVec.magnitude);

    }
}
