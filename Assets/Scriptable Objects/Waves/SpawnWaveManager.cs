using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaveManager : MonoBehaviour
{
    [SerializeField]
    List<SpawnerWaves> _waves = new List<SpawnerWaves>();
    int _waveIndex;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartWave());
    }
  IEnumerator StartWave()
    {
        
        while (true)
        {
            var currentWave = _waves[_waveIndex].wavesSequence;
            SpawnManager.Instance.TestWave(_waves[_waveIndex]);
        }
    }
}
