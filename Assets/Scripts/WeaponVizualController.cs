using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVizualController : MonoBehaviour
{


    [SerializeField] private Transform[] gunTransforms;
    // [SerializeField] private Transform pistol;
    // [SerializeField] private Transform revolver;
    // [SerializeField] private Transform autoRifile;
    // [SerializeField] private Transform shotgun;
    // [SerializeField] private Transform sniper;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchOnGuns(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchOnGuns(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchOnGuns(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchOnGuns(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            switchOnGuns(4);
        }
    }


    // private void switchOnGuns(Transform gunTransform)
    // {
    //     switchOffGuns();
    //     gunTransforms[gunTransform].gameObject.SetActive(true);
    // }

    private void switchOnGuns(int gunTransformNumber)
    {
        switchOffGuns();
        gunTransforms[gunTransformNumber].gameObject.SetActive(true);
    }



    private void switchOffGuns()
    {
        for (int i = 0; i < gunTransforms.Length; i++)
        {
            gunTransforms[i].gameObject.SetActive(false);
        }
    }
}
