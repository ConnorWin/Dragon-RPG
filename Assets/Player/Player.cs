using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    [SerializeField] int enemyLayer = 9;
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float damagePerHit = 10f;
    [SerializeField] float minTimeBetweenHits = 0.5f;
    [SerializeField] float maxAttackRange = 2f;

    GameObject currentTarget;
    float currentHealthPoints;
    CameraRaycaster cameraRaycaster;
    float lastHitTime = 0f;

    void Start()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
        currentHealthPoints = maxHealthPoints;
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if(layerHit == enemyLayer)
        {
            currentTarget = raycastHit.collider.gameObject;

            //cut the function if the target is out of range
            if ((currentTarget.transform.position - transform.position).magnitude > maxAttackRange)
                return;

            if(Time.time - lastHitTime > minTimeBetweenHits)
            {
                currentTarget.GetComponent<Enemy>().TakeDamage(damagePerHit);
                lastHitTime = Time.time;
            }

        }

    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        //if(currentHealthPoints <= 0) { Destroy(gameObject); }
    }
}
