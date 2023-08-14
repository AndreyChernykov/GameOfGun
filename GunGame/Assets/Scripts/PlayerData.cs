using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "PlayerData", order = 51)]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public List<int> playerLevel;
}
