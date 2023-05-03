using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public int damage;
    public float waitingTime = 0.32f;

}
