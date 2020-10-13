using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public override bool CheckFire()
    {
        return Input.GetButtonDown("Fire1") && nextFireTime < Time.time;
    }
}
