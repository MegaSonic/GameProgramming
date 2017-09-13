using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanGun : Gun {

    [SerializeField]
    private Transform emitTransform;

    [SerializeField]
    private DamageType damageType;

    [SerializeField]
    private int minDamage;

    [SerializeField]
    private int maxDamage;

    [SerializeField]
    private GameObject hitscanLine;

    public override void Shoot()
    {
        Ray ray = new Ray(emitTransform.position, emitTransform.forward);
        RaycastHit hit;
        Debug.DrawRay(emitTransform.position, emitTransform.forward, Color.red, 1f);

        if (Physics.Raycast(ray, out hit))
        {
            IHealth damageable = hit.collider.gameObject.GetComponent<IHealth>();

            if (damageable != null)
            {
                int damage = UnityEngine.Random.Range(minDamage, maxDamage + 1);
                GameObject newLine = Instantiate(hitscanLine);
                LineRenderer line = newLine.GetComponent<LineRenderer>();
                line.SetPosition(0, emitTransform.position);
                line.SetPosition(1, hit.collider.ClosestPoint(emitTransform.position));
                Debug.Log(damage);
                damageable.Damage(transform.root.gameObject, damageType, damage);

                if (EventManager.OnDamagedSomething != null)
                {
                    EventManager.OnDamagedSomething(transform.root.name, hit.collider.transform.root.name, damageType, damage);
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
#if UNITY_EDITOR
        if (minDamage > maxDamage)
        {
            Debug.LogWarning("Min damage greater than max damage in gun " + transform.name);
        }
#endif
    }

}
