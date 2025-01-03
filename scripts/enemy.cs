using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;//detection radius
    public float speed = 2.0f;//speed
    

    private Rigidbody2D rb;//physics
    private Vector2 movement;//movement
    private bool isRunning;//is running
    private Animator animator;//animator


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//call physics
        animator = GetComponent<Animator>();//call animations
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);//recive vector a, vector b and return thedistance between enemy and player

        if (distanceToPlayer < detectionRadius)//if distance to player is less than the detection radius
        {
            Vector2 direction = (player.position - transform.position).normalized;//the enemy walk to us 
            
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            
            movement = new Vector2(direction.x, 0);// the enemy can only follow us in x axis

            isRunning = true;


        }
        else//else if the player will move away from the detection radius.
        {
            movement = Vector2.zero;//the enemy stops following us
            isRunning = false;
        }

        rb.MovePosition(rb.position +  movement * speed *Time.deltaTime);//enemy movement

        animator.SetBool("isRunning", isRunning);//running animation

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))//if the enemy collides with the player, the enemy deals damage to the player
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);

            collision.gameObject.GetComponent<Character_Controller>().RecibeDanio(direccionDanio,1);
        }
    }

    void OnDrawGizmosSelected()//whach the radium 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,detectionRadius);
    }

}
