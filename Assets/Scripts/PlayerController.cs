using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    //Camera da Scene
    [SerializeField] Camera cam;

    //Relacionado a direção do player
    Vector2 mousePos;
    Vector2 lookDir;

    //Relacionado a atirar
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletForce = 10f;

    //Relacionado a atributos
    [SerializeField] int health = 1;

    //Relacionado a Eventos -> Para reduzir dependendicias entre classes
    public UnityEvent gameOver;
    void Start()
    {
        
    }

    
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
        }

    }

    void FixedUpdate()
    {
        //Vetor de direção entre a pósição do player e a posição do mouse
        lookDir = mousePos - (Vector2)this.gameObject.transform.position;
        //Angulo que o player tem de rotacionar
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //Rotacione no eixo z
        // TODO CONFERIR SE ISTO ESTA CERTO, ANTES ERA -90, ASSIM TALVEZ DE PROBLEMA NA TRAJETORIA DA BALA
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward); 

        this.gameObject.transform.rotation = rotation;


    }

    void ShootBullet() //TODO NO FUTURO USAR Object pooling
    {
        //Instancie uma Bala na posição do firepoint com o prefab de bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //Va no rigidbody dessa bala e adicione uma força de tamanho BulletForce
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }


    public void TakeDamage(int damage)
    {


        health -= damage;

        if (health <= 0)
        {
            gameOver.Invoke();
            Destroy(this.gameObject);
        }

    }
}
