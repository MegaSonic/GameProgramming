using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float timeTilDeath;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float headshotMultiplier;

    private Rigidbody rigid;
    private float deathTimer;
    private bool destroyed;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        deathTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        deathTimer += Time.deltaTime;
        if (deathTimer >= timeTilDeath)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (EventManager.OnEnemyDestroyed != null)
            {
                EventManager.OnEnemyDestroyed(other.name, other.transform);
            }

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void Shoot(Vector3 direction)
    {
        rigid.velocity = direction.normalized * speed;
    }
}
