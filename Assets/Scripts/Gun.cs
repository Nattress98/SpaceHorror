using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponStatus
{
    Normal,
    Reloading
}
public abstract class Gun : MonoBehaviour
{
    public LayerMask layerMask;
    public RecoilObject recoilObject;
    public GunStats stats;
    Animator handAnim;
    public WeaponStatus weaponStatus = WeaponStatus.Normal;
    public float nextFireTime = 0;
    public int magAmmo;

    Transform cameraTransform;

    public GameObject decal;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        handAnim = transform.parent.GetComponent<Animator>();
        magAmmo = stats.magazineSize;
        AmmoUI.UpdateAmmo(magAmmo);
    }
    public void Fire()
    {
        handAnim.Play("Fire");
        for (int i = 0; i < stats.bulletCount; i++)
        {
            Vector3 forwardVector = Vector3.forward;
            float deviation = UnityEngine.Random.Range(0f, stats.spread);
            float angle = UnityEngine.Random.Range(0f, 360f);
            forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
            forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
            forwardVector = cameraTransform.transform.rotation * forwardVector;

            Physics.Raycast(cameraTransform.position, forwardVector, out RaycastHit hit, 200f, layerMask); // change to projectile?
            if (hit.transform)
            {
                IHitReceiver hittable = hit.transform.GetComponent<IHitReceiver>();
                if (hittable != null)
                {
                    hittable.Hit(15.0f);
                }
                else
                {
                    //DrawDecals;
                }
                Transform dec = Instantiate(decal, hit.transform).transform;
                dec.position = hit.point;
            }
        }
        magAmmo--;

        nextFireTime = Time.time + stats.fireTime;
        AmmoUI.UpdateAmmo(magAmmo);

        recoilObject.recoil += stats.recoilTime;

        if (magAmmo == 0)
            Reload();

    }

    public void Reload()
    {
        nextFireTime = Time.time + stats.reloadTime;
        weaponStatus = WeaponStatus.Reloading;
        AmmoUI.SetReloading(nextFireTime);
    }
    private void Update()
    {
        switch (weaponStatus)
        {
            case WeaponStatus.Normal:
                if (CheckFire())
                {
                    if (magAmmo != 0)
                        Fire();
                    else
                        Reload();
                }
                break;
            case WeaponStatus.Reloading:
                if (nextFireTime < Time.time)
                    FinishReload();
                break;
        }
    }
    public abstract bool CheckFire();

    public void FinishReload()
    {
        weaponStatus = WeaponStatus.Normal;
        magAmmo = stats.magazineSize;
        AmmoUI.UpdateAmmo(magAmmo);
    }
}
