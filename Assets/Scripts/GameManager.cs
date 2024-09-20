using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] List<GameObject> spawnPos = new List<GameObject>();
    [SerializeField] int numbOfEnemys = 0;
    
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numbOfEnemys < 5)
        {
            Debug.Log("Quero Spawnar inimigo");
            int targetPos = Random.Range(1, spawnPos.Count);
            GameObject enemy = ObjectPool.instance.GetPooledEnemy();
            if (enemy != null)
            {
                enemy.transform.position = spawnPos[targetPos].transform.position;
                enemy.transform.rotation = spawnPos[targetPos].transform.rotation;
                enemy.SetActive(true);
                numbOfEnemys++;
            }
           
        }
    }

    public void enemyDied() 
    {
        score++;
        numbOfEnemys--;
    
    }


}
