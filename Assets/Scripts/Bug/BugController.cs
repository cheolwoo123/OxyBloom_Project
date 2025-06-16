using UnityEngine;

public class BugController : MonoBehaviour
{
    public BugEntity entity;
    public Transform target; //�Ĺ� �Ǵ� ����


    private Vector3 randomTargetPos; //Ÿ���� ������ġ
    private bool hasTargetPos = false; //Ÿ���������� ������ �ִ���

    [SerializeField] private float actCooldown = 2f; //������ Act ��Ÿ��
    private float lastActTime = -999f; //������ ����ð�

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

        Vector3 direction = (randomTargetPos - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            float rotationSpeed = 240f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

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
                    if (Time.time - lastActTime >= actCooldown)
                    {
                        Act(); // ���� Ư�� ����
                        lastActTime = Time.time;
                    }
                }
            }
        
    }
    public void Act()
    {

            Plant plant = target.GetComponentInChildren<Plant>();
            Debug.Log(plant.name);
            if (plant == null) return;

            switch (entity.bugData.pestType)
            {
                case PestType.PlantDegrowth:
                    // �Ĺ� ���� ����
                    plant.DegrowPlant(entity.bugData.growUp);
                    Debug.Log("���� ����");
                    break;
                case PestType.PlantDestruct:
                    // �Ĺ� ������(�Ĺ��� ü���� 0�̸� �ı�)
                    break;
                case PestType.OxygenLooter:
                    // ���� ����
                    break;
            }
        

    }

    public void TakeDamage(float damage)
    {
        entity.SetHP(entity.GetHP() - damage);

        if (entity.IsDead)
        {
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