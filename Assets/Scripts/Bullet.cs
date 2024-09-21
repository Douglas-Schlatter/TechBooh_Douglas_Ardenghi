using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Relacionado a dano da bala
    [SerializeField] private int damage = 1;
   
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    /// <summary>
    /// Usado para caso a bullet entre em colisão com outro objeto, se ele tiver
    /// um script de vida, dê dano àquele objeto, assim a bala pode ser reescrita para
    /// ser usada em outros lugares, por exemplo quebrar cenarios, etc.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.TakeDamage(damage);
            
        }

        this.gameObject.SetActive(false);

    }
}
