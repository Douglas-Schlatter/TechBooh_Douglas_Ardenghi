using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int health = 2;
    void Start()
    {
        
    }

    public void TakeDamage(int damage)
    {


        health -= damage;

        if (health <= 0)
        {
            Die();
        }

    }
    
    /// <summary>
    /// Posterirmente usarei essa funcao para atualizar o score no gamecontroller e UI por meio de evento
    /// </summary>
    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
