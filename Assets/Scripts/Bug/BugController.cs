using UnityEngine;

public class BugController : MonoBehaviour
{
    public BugEntity entity;
    public Transform target; //식물 또는 벌레


    private Vector3 randomTargetPos; //타깃의 랜덤위치
    private bool hasTargetPos = false; //타깃포지션을 가지고 있는지

    [SerializeField] private float actCooldown = 2f; //벌레들 Act 쿨타임
    private float lastActTime = -999f; //마지막 실행시간

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

                Collider2D plantCollider = target.GetComponent<Collider2D>();
                if (plantCollider != null && plantCollider.OverlapPoint(transform.position))
                {
                    if (Time.time - lastActTime >= actCooldown)
                    {
                        Act(); // 벌레 특성 실행
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
                    // 식물 성장 억제
                    plant.DegrowPlant(entity.bugData.growUp);
                    Debug.Log("성장 억제");
                    break;
                case PestType.PlantDestruct:
                    // 식물 데미지(식물의 체력이 0이면 파괴)
                    break;
                case PestType.OxygenLooter:
                    // 익충 공격
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
        // 오브젝트 풀 썼을때
        //gameObject.SetActive(false);
        Destroy(gameObject);

    }
}