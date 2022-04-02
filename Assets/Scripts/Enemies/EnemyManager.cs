using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager EM;

    public List<Enemy> enemies = new List<Enemy>();

    public void NewSpawn(Enemy e){
        enemies.Add(e);
        // TODO: Reset some enemy positions/Set them to patrol if too many are spawned.
    }

    void Awake()
    {
        if(EM != null) GameObject.Destroy(EM);
        else EM = this;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        
    }
}
