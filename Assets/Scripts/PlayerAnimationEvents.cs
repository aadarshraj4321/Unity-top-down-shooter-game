using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{

    private WeaponVizualController weaponVizualController;

    void Start()
    {
        weaponVizualController = GetComponentInParent<WeaponVizualController>();
    }


    public void reloadIsOver()
    {
        weaponVizualController.returnRigWeightToOne();
    }


}
