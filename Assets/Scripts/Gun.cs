using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Gun : MonoBehaviour
{

    [SerializeField]
    private Transform emitPoint;

    [SerializeField]
    private Transform emitDirection;

    [SerializeField]
    private GameObject bulletObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bulletInstance = GameObject.Instantiate(bulletObject, emitPoint.position, Quaternion.identity);
            bulletInstance.SetActive(true);
            bulletInstance.GetComponent<Bullet>().Shoot(emitPoint.forward);
            // bulletInstance.GetComponent<Bullet>().Shoot(emitDirection.forward);
            bulletInstance.gameObject.tag = this.transform.root.tag;

            if (EventManager.OnPlayerFired != null)
            {
                EventManager.OnPlayerFired(this.transform.root);
            }
        }
    }
}
