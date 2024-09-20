using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //Tem que ser um singleton para ser referenciado em diversos codigos do projeto
    public static ObjectPool instance;
    //Relacionado às bullets
    private List<GameObject> poolBullets = new List<GameObject>();
    [SerializeField]  private int numbBullets = 100; //-> coloquei um numero grande para caso o player queira spamar as bullets

    [SerializeField] private GameObject bulletPrefab;
    //Relacionado aos inimigos
    private List<GameObject> poolEnemys = new List<GameObject>();
    [SerializeField]  private int numbEnemys = 20;

    [SerializeField] private GameObject enemyPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < numbBullets; i++)
        {
            GameObject obj = Instantiate(bulletPrefab,this.gameObject.transform);
            obj.SetActive(false);
            poolBullets.Add(obj);
        }

        for (int i = 0; i < numbEnemys; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, this.gameObject.transform);
            obj.SetActive(false);
            poolBullets.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Esse metodo procurará na lista de bullets intanciadas se existe uma 
    /// que não esteja ativa em scene, isso significa que ela esta disponivel para
    /// ser usada, dessa forma podemos recicla-la
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < poolBullets.Count; i++)
        {
            if (!poolBullets[i].activeInHierarchy)
            {
                return poolBullets[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Esse metodo procurará na lista de inimigos intanciados se existe um 
    /// que não esteja ativo em scene, isso significa que ele esta disponivel para
    /// ser usado, dessa forma podemos recicla-lo
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledEnemy()
    {
        for (int i = 0; i < poolEnemys.Count; i++)
        {
            if (!poolEnemys[i].activeInHierarchy)
            {
                return poolEnemys[i];
            }
        }
        return null;
    }

}
