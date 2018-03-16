using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRadius = 5f;
    [SerializeField] float damagePerShot = 1f;
    [SerializeField] float secondsBetweenShots = 0.5f;
    [SerializeField] float moveRadius = 5f;
    [SerializeField] GameObject projectileToUse;
    [SerializeField] GameObject projectileSocket;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);

    bool isAttacking = false;
    float currentHealthPoints;
    AICharacterControl aiCharacterControl = null;
    GameObject player = null;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        if(currentHealthPoints <= 0) { Destroy(gameObject); }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
        currentHealthPoints = maxHealthPoints;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if(distanceToPlayer <= attackRadius && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("SpawnProjectile", 0f, secondsBetweenShots); //invoke real coroutine
        }

        if(distanceToPlayer > attackRadius)
        {
            isAttacking = false;
            CancelInvoke();
        }

        if (distanceToPlayer <= moveRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }
    void SpawnProjectile()
    {
        GameObject newShot = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        Projectile projectileComponent = newShot.GetComponent<Projectile>();
        projectileComponent.Set(damagePerShot);

        Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
        float projectileSpeed = projectileComponent.projectileSpeed;
        newShot.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
        print(unitVectorToPlayer);
    }

    void OnDrawGizmos()
    {
        //Draw attack sphere
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        //Draw move sphere
        Gizmos.color = new Color(0f, 0f, 255f, .5f);
        Gizmos.DrawWireSphere(transform.position, moveRadius);
    }
}
