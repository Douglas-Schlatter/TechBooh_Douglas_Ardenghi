using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    //Camera da Scene
    [SerializeField] Camera cam;

    //Relacionado a direção do player
    private Vector2 mousePos;
    private Vector2 lookDir;

    //Relacionado a atirar
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletForce = 10f;

    //Relacionado a atributos
    [SerializeField] private int health = 1;

    //Relacionado a Eventos -> Para reduzir dependendicias entre classes
    public UnityEvent onGameOver;
    void Start()
    {
        // Colocamos o gamecontroler como observador/listener desse evento, assim podemos chama-lo quando o jogador morrer
        onGameOver.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().CallUI);
    }

    
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Se o jogo não estiver pausado, capte inputs
        if (Time.timeScale > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootBullet();
            }
        }


    }

    void FixedUpdate()
    {
        //Vetor de direção entre a pósição do player e a posição do mouse
        lookDir = mousePos - (Vector2)this.gameObject.transform.position;
        //Angulo que o player tem de rotacionar
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //Rotacione no eixo z
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward); 

        this.gameObject.transform.rotation = rotation;


    }

    /// <summary>
    /// Função responsavel por utilizar Object Polling para reciclar balas
    /// atiradas pelo jogador.
    /// </summary>
    void ShootBullet() 
    {
        //Tente pegar uma bala disponivel para ser reciclada
        GameObject bullet = ObjectPool.instance.GetPooledBullet();

        if (bullet != null)
        {
            //se existir coloque-a na posição do firePoint , a ative e 
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
            //Va no rigidbody dessa bala e adicione uma força de tamanho BulletForce
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        }

    }


    /// <summary>
    /// Essa será o metodo que lidará com os danos recebidos pelo player
    /// posteriormente ativando o evento de "onGameOver"
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {


        health -= damage;

        if (health <= 0)
        {
            onGameOver.Invoke();
            health = 1;
        }

    }
}
