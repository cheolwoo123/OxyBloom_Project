using UnityEngine;

public class BugController : MonoBehaviour
{
    public BugEntity entity;
    public Transform target; //�Ĺ� �Ǵ� ����


    private Vector3 randomTargetPos; //Ÿ���� ������ġ
    private bool hasTargetPos = false; //Ÿ���������� ������ �ִ���

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
            // ���� - �Ĺ� �������� �̵�
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

            // �����ϸ� �ٽ� ��ǥ �ʱ�ȭ
            if (Vector3.Distance(transform.position, randomTargetPos) < 0.1f)
            {
                hasTargetPos = false;
            }
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
        entity.SetHP(entity.GetHP() - damage);
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