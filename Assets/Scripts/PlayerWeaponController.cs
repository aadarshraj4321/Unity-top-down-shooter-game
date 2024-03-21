using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{


    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        player.controls.Character.Fire.performed += context => shoot();
    }



    private void shoot()
    {
        GetComponentInChildren<Animator>().SetTrigger("Fire");
    }


}
