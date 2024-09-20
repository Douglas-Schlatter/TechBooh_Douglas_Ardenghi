using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int health = 2;

    public UnityEvent onDeath;
    void Start()
    {
        onDeath.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().enemyDied);
    }

    public void TakeDamage(int damage)
    {


        health -= damage;

        if (health <= 0)
        {
            onDeath.Invoke();

            this.gameObject.SetActive(false);
        }

    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
