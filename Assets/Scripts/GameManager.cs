using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Relacionado ao Score
    [SerializeField] int score = 0;

    //Relacionado ao sistema de spawn do jogo
    [SerializeField] List<GameObject> spawnPos = new List<GameObject>();
    [SerializeField] int numbOfEnemys = 0;
    int lastTargetPos;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO um bug esta acontecendo que multiplos inimigos estão spawnando no mesmo lugar  no inicio da scene

        //Quero manter sempre 5 inimigos na tela
        if (numbOfEnemys < 5)
        {
            //Debug.Log("Quero Spawnar inimigo");
            //Sorteie um spawnpoint que não seja o ultimo selecionado
            int targetPos = Random.Range(1, spawnPos.Count);
            if (targetPos != lastTargetPos)
            {
                lastTargetPos = targetPos;
                //Tente pegar um inimigo disponivel para ser reciclado
                GameObject enemy = ObjectPool.instance.GetPooledEnemy();
                if (enemy != null)
                {
                    //se existir coloque-o na posição de spawn e o o ative
                    enemy.transform.position = spawnPos[targetPos].transform.position;
                    enemy.transform.rotation = spawnPos[targetPos].transform.rotation;
                    enemy.SetActive(true);
                    numbOfEnemys++;
                }
            }
        }
    }
    private void FixedUpdate()
    {
   
        //Score não é zero e é divisivel por 10
        if ((score != 0 )&&(score % 10 == 0))
        {
            Debug.Log("Troque de scene");
            SceneManager.LoadScene("BattleScene");
        }

    }

    /// <summary>
    /// Essa função sera utilizada para que o GameManager possa reagir ao eventod e um inimigo
    /// morrendo, fazendo com que o score aumente o numero de imnimigos diminua sem nenhuma conexão
    /// direta com outros scripts
    /// </summary>
    public void enemyDied() 
    {
        score++;
        numbOfEnemys--;
    
    }


}
