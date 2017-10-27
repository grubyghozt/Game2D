using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponList",menuName = "myAssets/weaponList")]
public class weaponsList : ScriptableObject {
    public List<GameObject> weapons;
}
