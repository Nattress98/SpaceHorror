using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    private static TextMeshProUGUI text;
    static bool reloading = false;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public static void UpdateAmmo(int ammo)
    {
        reloading = false;
        text.SetText(ammo.ToString());
    }
    static float reloadTime;
    public static void SetReloading(float time)
    {
        reloading = true;
        reloadTime = time;
    }
    private void Update()
    {

        if (reloading)
        {
            string s = (reloadTime - Time.time).ToString();
            if (s.Length > 3)
                s = s.Substring(0, 3);
            text.SetText("Reloading: " + s);
        }
    }
}
