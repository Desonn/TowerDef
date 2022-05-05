using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "waveSpawner.asset", menuName = "Scriptable Objects/new wave")]
public class SpawnerWaves : ScriptableObject
{
    public List<GameObject> wavesSequence;
  
}
