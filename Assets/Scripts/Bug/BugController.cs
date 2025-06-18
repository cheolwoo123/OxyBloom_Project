using System.Collections;
using UnityEngine;

public class BugController : MonoBehaviour
{
    public BugEntity entity;
    public Transform target; 
    private Plant plant;

    public Animator bugAnimator; 

    private Vector3 randomTargetPos; //Ÿ���� ������ġ
    private bool hasTargetPos = false; //Ÿ���������� ������ �ִ���

    [SerializeField] private float actCooldown = 2f; //������ Act ��Ÿ��
    private float lastActTime = -999f; //������ ����ð�

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
            // ������ ���� �ȿ����� ��� ������
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

                // Act �ֱ������� ����
                if (Time.time - lastActTime >= actCooldown)
                {
                    Act();
                    lastActTime = Time.time;
                }
            }

            // ���� ȸ��
            Vector3 dir = (randomTargetPos - transform.position).normalized;
            if (dir != Vector3.zero)
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 240f * Time.deltaTime);
            }

            return; // �Ʒ� �Ϲ� ���� �̵��� �ǳʶ�
        }

        // �Ϲ� ���� �̵�
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
                    // �Ĺ� ���� ����
                    plant.DegrowPlant(entity.bugData.growUp);
                    Debug.Log("���� ����");
                    break;
                case PestType.PlantDestruct:
                // �Ĺ� ������(�Ĺ��� ü���� 0�̸� �ı�)
                    plant.DegrowPlant(entity.bugData.growUp);
                    break;
                case PestType.OxygenLooter:
                // ��� ����
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