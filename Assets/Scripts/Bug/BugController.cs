using UnityEngine;

public class BugController : MonoBehaviour
{
    private BugEntity entity;
    private Transform target; //식물

    private void Awake()
    {
        entity = GetComponent<BugEntity>();
    }
    public void Init(BugScriptObject bugData, Transform target = null)
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
            // 해충: 식물 방향으로 이동
            transform.position = Vector3.MoveTowards(transform.position, target.position, entity.bugData.speed * Time.deltaTime);
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
        entity.SetHP(entity.CurrentHP - damage);
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