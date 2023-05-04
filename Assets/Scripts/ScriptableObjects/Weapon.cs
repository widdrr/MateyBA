using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    glock,
    crowbar
}

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public WeaponType type;
    public int damage;
    public float waitingTime = 0.32f;

}
