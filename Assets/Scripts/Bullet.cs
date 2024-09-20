using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Usado para caso a bullet entre em colisão com outro objeto, se ele tiver
    /// um script de vida, de dano àquele objeto, assim a bala pode ser reescrita para
    /// ser usada em outros lugares
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if (col.gameObject.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.TakeDamage(damage);
            
        }
        //Destroy(gameObject); // Sem object pooling
        this.gameObject.SetActive(false);
        /*
         Outra versão que usa como refencia tags
        if (col.tag == "Enemy")
        {
            Health targetHealth = col.GetComponent<Health>();
            targetHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
        */
    }
}
