using UnityEngine;

public class BugController : MonoBehaviour
{
    private BugEntity entity;
    private Transform target; //�Ĺ�

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
            // ����: �Ĺ� �������� �̵�
            transform.position = Vector3.MoveTowards(transform.position, target.position, entity.bugData.speed * Time.deltaTime);
        }
        else if (entity.bugData.category == BugCategory.Beneficial)
        {
            // ����: ���� �̵�
        }
    }
    public void Act()
    {
        if (entity.bugData.category == BugCategory.Pest)
        {
            switch (entity.bugData.pestType)
            {
                case PestType.PlantDegrowth:
                    // �Ĺ� ���� ����
                    break;
                case PestType.PlantDestruct:
                    // �Ĺ� ������(�Ĺ��� ü���� 0�̸� �ı�)
                    break;
                case PestType.KillBeneficial:
                    // ���� ����
                    break;
            }
        }
        else
        {
            switch (entity.bugData.beneficialType)
            {
                case BeneficialType.PromoteGrowth:
                    // �Ĺ� ���� ����
                    break;
                case BeneficialType.ControlOxygen:
                    // ��� ����
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
        // ������Ʈ Ǯ�� ��ȯ
        gameObject.SetActive(false);
    }
}