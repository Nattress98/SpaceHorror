using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/GunStats")]
public class GunStats : ScriptableObject
{
    public int magazineSize = 6; //-1 = infinity
    public float damage;
    public float reloadTime;
    public float fireTime;
    public float spread = 0;
    public float recoilTime = 0.1f;
    [Range(1, 20)]
    public int bulletCount = 1;
}
