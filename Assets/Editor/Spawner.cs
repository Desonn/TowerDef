using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




public class Spawner : EditorWindow

{
    [SerializeField]
    SpawnerWaves[] _waves;
    [SerializeField]
    Texture coin;
    [SerializeField]
    GameObject[] mech;
    Rect coinsection;
    int addfunds;

    static int warfunds;
    float iconsize = 80f;
    [MenuItem("Window/WaveSpawner")]
    static void Start()
    {
        var window = EditorWindow.GetWindow<Spawner>();
        window.Show();
    }
    public void Init()
    {
        
    }
    public void OnGUI()
    {

        var so = new SerializedObject(this);
        var prop = so.FindProperty("_waves");
        EditorGUILayout.PropertyField(prop, true);
        so.ApplyModifiedProperties();

        if (GUILayout.Button("TestWave"))
        {
            SpawnManager.Instance.TestWave(_waves[0]);
        }
        warfunds = EditorGUILayout.IntField("Add WarFunds:", addfunds);
        if (GUILayout.Button("Add Funds"))
        {
            WarFunds.Instance.CurrentWarFunds += addfunds;
        }
       for(int i=0; i<2; i++)
        {
            mech[i] = (GameObject)EditorGUILayout.ObjectField("Mechs", mech[i], typeof(GameObject));
        }
        Undo.RecordObject(this, "Some Random text");
        if (GUILayout.Button("Create Mech1"))
        {
            Instantiate(mech[0], Vector3.zero, Quaternion.identity);
        }
        if (GUILayout.Button("Create Mech2"))
        {
            Instantiate(mech[1], Vector3.zero, Quaternion.identity);
        }
        EditorUtility.SetDirty(this);

    }

    private void OnEnable()
    {
        mech = new GameObject[2];
    }
   
}
///objectfield asserdatabase