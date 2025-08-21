using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float health; //체력 
    public float MaxHealth; //최대 체력
    public RuntimeAnimatorController[] animatorCon; //애니메이션 컨트롤러 배열 
    public Rigidbody2D Target;

    bool isLive; //몬스터의 생사 여부 

    Rigidbody2D rb;
    SpriteRenderer Spriter;
    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive) return; //몬스터가 살아있지 않다면 리턴 아니면 밑의 코드 실행

        Vector2 direction = (Target.position - rb.position).normalized; //플레이어와의 거리 계산 
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime); //거리* 속도 * 시간 간격을 이용하여 몬스터 이동
        rb.velocity = Vector2.zero; //정해진 방향으로 이동하기 위해 속도를 0으로 초기화    

    }
    void LateUpdate()
    {
        if (!isLive) return; // //몬스터가 살아있지 않다면 리턴 아니면 밑의 코드 실행
        Spriter.flipX = Target.position.x < rb.position.x; //플레이어 보다 왼쪽에 있으면 몬스터가 왼쪽을 바라보게 하고 오른쪽에 있으면 오른쪽을 바라보게 함


    }

    void OnEnable()
    {
        Target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true; //살아있음으로 초기화
        health = MaxHealth; //최대 체력으로 초기화

    }
    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animatorCon[data.spriteType]; // 스폰 데이터에 지정된 스프라이트 타입에 맞는 애니메이션 컨트롤러를 할당
        speed = data.speed; // 몬스터의 이동 속도 초기화
        MaxHealth = data.health; //몬스터의 최대 체력 초기화
        health = MaxHealth; //몬스터의 체력 초기화  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) //Bullet 태그를 가진 오브젝트와 충돌했을 때
        {
          health -=  collision.GetComponent<Bullet>().damage;//Bullet 스크립트에서 데미지값을 가져와 연산
          if(health <= 0) //0보다 체력이 작아지면 
            {
                Dead(); //Dead 메소드 호출
            }
            else
            {
                return; //체력이 남았다면 리턴
            }
        }
        else if (collision.CompareTag("MeleeWeapon"))
        {
            health -= collision.GetComponent<Weapon>().damage; //MeleeWeapon 스크립트에서 데미지값을 가져와 연산
            if (health <= 0) //0보다 체력이 작아지면 
            {
                Dead(); //Dead 메소드 호출
            }
            else
            {
                return; //체력이 남았다면 리턴
            }
        }
    }
    void Dead()
    {
        gameObject.SetActive(false); //오브젝트 비활성화
        animator.SetTrigger("Hit"); //윗줄로 인해 애니메이션이 재생되지 않지만 Hit 트리거를 설정하여 애니메이션이 재생되도록 함
    }
}

