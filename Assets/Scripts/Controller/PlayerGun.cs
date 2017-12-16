using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour {

    public List<Gun> equippedGuns;


    public Gun currentGun;

	// Use this for initialization
	void Start () {
        if (!currentGun)
        {
            currentGun = equippedGuns[0];
            currentGun.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            if (currentGun != null)
            {
                currentGun.Shoot();

                if (EventManager.OnPlayerFiredGun != null)
                {
                    EventManager.OnPlayerFiredGun(this.transform.root, currentGun.transform.name);
                }
            }
            else
            {
                Debug.LogWarning("Attempting to shoot a null gun");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            EquipGun(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipGun(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipGun(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipGun(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EquipGun(4);
        }
    }

    public void EquipGun(int slot)
    {
        if (currentGun != equippedGuns[slot])
        {
            currentGun.gameObject.SetActive(false);
            equippedGuns[slot].gameObject.SetActive(true);
            currentGun = equippedGuns[slot].gameObject.GetComponent<Gun>();
        }
    }
}
