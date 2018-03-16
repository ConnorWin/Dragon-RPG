using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float projectileSpeed = 10;
    float damageCaused { get; set; }

    public void Set(float val) { damageCaused = val; }
    public float Get() { return damageCaused; }

    void OnTriggerEnter(Collider collider)
    {
        Component damageableComponent = collider.gameObject.GetComponent(typeof(IDamageable));
        print("projectile:" + damageableComponent);
        if(damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage(damageCaused);
        }
    }
}
