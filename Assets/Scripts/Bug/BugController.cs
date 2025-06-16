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

                Collider2D plantCollider = target.GetComponent<Collider2D>();
                if (plantCollider != null && plantCollider.OverlapPoint(transform.position))
                {
                    Act(); // ���� Ư�� ����
                }
            }
        }
        else if (entity.bugData.category == BugCategory.Beneficial)
        {
            Camera cam = target.GetComponent<Camera>();
        }
    }
    public void Act()
    {
        if (entity.bugData.category == BugCategory.Pest)
        {
            Plant plant = target.GetComponent<Plant>();

            if (plant == null) return;

            switch (entity.bugData.pestType)
            {
                case PestType.PlantDegrowth:
                    // �Ĺ� ���� ����
                    plant.DegrowPlant(-entity.bugData.growUp);
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
            Plant plant = target.GetComponent<Plant>();
            if (plant == null) return;

            switch (entity.bugData.beneficialType)
            {
                case BeneficialType.PromoteGrowth:
                    // �Ĺ� ���� ����
                    plant.GrowPlant(entity.bugData.growUp);
                    break;
                case BeneficialType.ControlOxygen:
                    // ��� ����
                    break;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        entity.SetHP(entity.GetHP() - damage);

        if (entity.IsDead)
        {
            Debug.Log("Bug died");
            Die();
        }
    }

   

    private void Die()
    {
        // ������Ʈ Ǯ ������
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}