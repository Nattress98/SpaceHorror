using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    public override bool CheckFire()
    {
        return Input.GetButton("Fire1") && Time.time > nextFireTime;
    }
}
