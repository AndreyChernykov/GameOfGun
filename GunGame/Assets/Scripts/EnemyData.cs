using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "EnemyData", order = 51)]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public int level { get; private set; }
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public float moveSpeed { get; private set; }
    [field: SerializeField] public int points { get; private set; }
    [field: SerializeField] public int dropProbability { get; private set; }
    [field: SerializeField] public List<GameObject> items;
}

