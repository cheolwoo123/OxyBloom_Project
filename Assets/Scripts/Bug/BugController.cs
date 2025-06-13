using UnityEngine;

public class BugController : MonoBehaviour
{
    public BugEntity entity;
    public Transform target; //식물 또는 벌레


    private Vector3 randomTargetPos; //타깃의 랜덤위치
    private bool hasTargetPos = false; //타깃포지션을 가지고 있는지

    private void Awake()
    {
        entity = GetComponent<BugEntity>();
    }

    public void Setup(BugScriptObject bugData, Transform target)
    {
        entity.Init(bugData);   
        this.target = target;    
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (entity.bugData.category == BugCategory.Pest && target != null)
        {
            // 해충 - 식물 방향으로 이동
            if (!hasTargetPos)
            {
                Collider2D plantCollider = target.GetComponent<Collider2D>();
                if(plantCollider != null)
                {
                    Bounds bounds = plantCollider.bounds;
                    float x = Random.Range(bounds.min.x, bounds.max.x);
                    float y = Random.Range(bounds.min.y, bounds.max.y);

                    randomTargetPos = new Vector3(x, y, 0);
                    hasTargetPos = true;
                }
                
            }
            transform.position = Vector3.MoveTowards(transform.position, randomTargetPos, entity.GetSpeed() * Time.deltaTime);

            // 도착하면 다시 목표 초기화
            if (Vector3.Distance(transform.position, randomTargetPos) < 0.1f)
            {
                hasTargetPos = false;
            }
        }
        else if (entity.bugData.category == BugCategory.Beneficial)
        {
            // 익충: 랜덤 이동
        }
    }
    public void Act()
    {
        if (entity.bugData.category == BugCategory.Pest)
        {
            switch (entity.bugData.pestType)
            {
                case PestType.PlantDegrowth:
                    // 식물 성장 억제
                    break;
                case PestType.PlantDestruct:
                    // 식물 데미지(식물의 체력이 0이면 파괴)
                    break;
                case PestType.KillBeneficial:
                    // 익충 공격
                    break;
            }
        }
        else
        {
            switch (entity.bugData.beneficialType)
            {
                case BeneficialType.PromoteGrowth:
                    // 식물 성장 증가
                    break;
                case BeneficialType.ControlOxygen:
                    // 산소 조절
                    break;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        entity.SetHP(entity.GetHP() - damage);
        if (entity.IsDead)
        {
            Die();
        }
    }

   

    private void Die()
    {
        // 오브젝트 풀로 반환
        gameObject.SetActive(false);
    }
}