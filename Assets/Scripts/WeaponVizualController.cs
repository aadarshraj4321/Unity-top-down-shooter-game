using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponVizualController : MonoBehaviour
{


    [SerializeField] private Transform[] gunTransforms;
    // [SerializeField] private Transform pistol;
    // [SerializeField] private Transform revolver;
    // [SerializeField] private Transform autoRifile;
    // [SerializeField] private Transform shotgun;
    // [SerializeField] private Transform sniper;



    private Transform currentGun;


    [Header("Rig")]
    [SerializeField]
    private float rigIncreaseStep;
    private bool rigShouldBeIncreased;


    [Header("Left HandIK")]
    [SerializeField] private Transform leftHand;


    private Animator anim;
    private Rig rig;

    private void Start()
    {
        switchOnGuns(0);
        anim = GetComponentInChildren<Animator>();
        rig = GetComponentInChildren<Rig>();
    }



    private void Update()
    {
        checkWeaponSwitch();


        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
            rig.weight = 0;
        }

        // if (Input.GetKey(KeyCode.K))
        // {
        //     rigShouldBeIncreased = true;
        // }


        if (rigShouldBeIncreased)
        {
            rig.weight += rigIncreaseStep * Time.deltaTime;
            if (rig.weight >= 1)
            {
                rigShouldBeIncreased = false;
            }
        }

    }


    public void returnRigWeightToOne() => rigShouldBeIncreased = true;


    private void checkWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchOnGuns(0);
            switchAnimationLayer(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchOnGuns(1);
            switchAnimationLayer(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchOnGuns(2);
            switchAnimationLayer(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchOnGuns(3);
            switchAnimationLayer(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            switchOnGuns(4);
            switchAnimationLayer(3);
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
        currentGun = gunTransforms[gunTransformNumber];

        attachLeftHand();
        //switchAnimationLayer(gunTransformNumber);
    }



    private void switchOffGuns()
    {
        for (int i = 0; i < gunTransforms.Length; i++)
        {
            gunTransforms[i].gameObject.SetActive(false);
        }
    }


    private void attachLeftHand()
    {
        Transform targetTransform = currentGun.GetComponentInChildren<LeftHandTargetTransform>().transform;

        leftHand.localPosition = targetTransform.localPosition;
        leftHand.localRotation = targetTransform.localRotation;
    }



    private void switchAnimationLayer(int layerIndex)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(layerIndex, 1);
    }




}
