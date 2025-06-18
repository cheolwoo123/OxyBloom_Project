using System.Collections;
using UnityEngine;

public class BugController : MonoBehaviour
{
    public BugEntity entity;
    public Transform target; 
    private Plant plant;

    public Animator bugAnimator; 

    private Vector3 randomTargetPos; //타깃의 랜덤위치
    private bool hasTargetPos = false; //타깃포지션을 가지고 있는지

    [SerializeField] private float actCooldown = 2f; //벌레들 Act 쿨타임
    private float lastActTime = -999f; //마지막 실행시간

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        entity = GetComponent<BugEntity>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void Setup(BugScriptObject bugData, Transform target)
    {
        entity.Init(bugData);   
        this.target = target;
        plant = target.GetComponentInChildren<Plant>();
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (entity.bugData.pestType == PestType.OxygenLooter)
        {
            // 고정된 범위 안에서만 계속 움직임
            if (!hasTargetPos)
            {
                float x = Random.Range(-11f, -5f);
                float y = Random.Range(-5f, 5f);
                randomTargetPos = new Vector3(x, y, 0f);
                hasTargetPos = true;
            }

            transform.position = Vector3.MoveTowards(transform.position, randomTargetPos, entity.GetSpeed() * Time.deltaTime);

            if (Vector3.Distance(transform.position, randomTargetPos) < 0.1f)
            {
                hasTargetPos = false;

                // Act 주기적으로 실행
                if (Time.time - lastActTime >= actCooldown)
                {
                    Act();
                    lastActTime = Time.time;
                }
            }

            // 방향 회전
            Vector3 dir = (randomTargetPos - transform.position).normalized;
            if (dir != Vector3.zero)
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 240f * Time.deltaTime);
            }

            return; // 아래 일반 해충 이동은 건너뜀
        }

        // 일반 해충 이동
        Vector3 direction = (randomTargetPos - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            float rotationSpeed = 240f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (!hasTargetPos)
        {
            Collider2D plantCollider = target.GetComponent<Collider2D>();
            if (plantCollider != null)
            {
                Bounds bounds = plantCollider.bounds;
                float x = Random.Range(bounds.min.x, bounds.max.x);
                float y = Random.Range(bounds.min.y, bounds.max.y);
                randomTargetPos = new Vector3(x, y, 0);
                hasTargetPos = true;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, randomTargetPos, entity.GetSpeed() * Time.deltaTime);

        if (Vector3.Distance(transform.position, randomTargetPos) < 0.1f)
        {
            hasTargetPos = false;

            Collider2D plantCollider = target.GetComponent<Collider2D>();
            if (plantCollider != null && plantCollider.OverlapPoint(transform.position))
            {
                if (Time.time - lastActTime >= actCooldown)
                {
                    Act();
                    lastActTime = Time.time;
                }
            }
        }
    }
    public void Act()
    {

            Debug.Log(plant.name);
            if (plant == null) return;

            switch (entity.bugData.pestType)
            {
                case PestType.PlantDegrowth:
                    // 식물 성장 억제
                    plant.DegrowPlant(entity.bugData.growUp);
                    Debug.Log("성장 억제");
                    break;
                case PestType.PlantDestruct:
                // 식물 데미지(식물의 체력이 0이면 파괴)
                    plant.DegrowPlant(entity.bugData.growUp);
                    break;
                case PestType.OxygenLooter:
                // 산소 감소
                    GameManager.Instance.SetOxygen(-entity.bugData.oxygenAmount);
                    break;
            }
        

    }

    public void TakeDamage(float damage)
    {
        entity.SetHP(entity.GetHP() - damage);

        StartCoroutine(FlashRed());

        if (entity.IsDead)
        {
            Die();
        }
    }

   

    public void Die()
    {
        bugAnimator.SetBool("isDead", true);

        this.enabled = false;

        Invoke(nameof(DestroyBug), 0.5f);
    }

    private void DestroyBug()
    {
        Destroy(gameObject);
    }

    private IEnumerator FlashRed()
    {
        bugAnimator.enabled = false;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
        bugAnimator.enabled = true;
    }
}