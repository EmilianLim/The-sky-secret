using Unity.VisualScripting;
using UnityEngine;


//character controller
public class Character_Controller : MonoBehaviour
{
    //variable declaration
    public float Speed;//velocity
    public float FuerzaSalto;//the force of the jump
    public float fuerzaRebote = 10f;//the force of rebound
    public int saltosMaximos;// maximum jumps
    public LayerMask capaSuelo;//floor


    private BoxCollider2D boxCollider;//Collisions
    private bool recibiendoDanio;//damage
    private Rigidbody2D miRigidbody;//movement
    private bool MirandoDerecha = true;//states that you are looking to the right but if not, the change will be made to the code
    private int saltosRestantes;//remaining jumps
    private Animator animator;//animations

    //the functions in this function are called when the program start
    private void Start()
    {
    
        miRigidbody = GetComponent<Rigidbody2D>();//physics
        boxCollider = GetComponent<BoxCollider2D>();//hitbox
        saltosRestantes = saltosMaximos;//maximum jums start in 2 = ramaining jumps
        animator = GetComponent<Animator>();//call the animations
    }

    //update is called once per frame
    void Update()
    {   
        
        ProcesarMovimiento();//call function "ProcesarMovimiento()"
        ProcesarSalto();//call function "ProcesarSalto()"
        
        animator.SetBool("recibeDanio", recibiendoDanio);//If the main character collides with an enemy the crash animation is activated

    }

 

    bool EstaEnSuelo() //function bool tell us if the main character is in the floor
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x,boxCollider.bounds.size.y), 0f,Vector2.down, 0.2f, capaSuelo);//We made a box that, when it hits the ground, detects the ground and knows that it is on the ground.
        return raycastHit.collider != null;//if raycast.collider has not collided with anything it returns false
    
    }

    

    //Process  main character jump
    void ProcesarSalto()
    {
        
        if (EstaEnSuelo())//if the main character is in the floor the remaining jumps restart
        {
            saltosRestantes = saltosMaximos;
            
        }


        if (Input.GetKeyDown(KeyCode.Space)&& !recibiendoDanio && saltosRestantes > 0 ) //if the user tab the space bar, the user can jump as long as you don't receive damage and have remaining jumps
        {
            saltosRestantes--;//remaining jumps -1
            miRigidbody.linearVelocity = new Vector2(miRigidbody.linearVelocity.x, 0f);//the physics rest the force of jump, but in this line
            miRigidbody.AddForce(Vector2.up * FuerzaSalto,ForceMode2D.Impulse);// vertical force 
        
        }
    }



    //permite el movimiento del personaje, asi como darle una velocidad la cual es ajustable desde unity
    void ProcesarMovimiento()
    {
        // Logica de movimiento
        float inputMovimiento = Input.GetAxis("Horizontal");//the user can use the direction bar right and left

        if (inputMovimiento != 0)
        {
            animator.SetBool("inRunning",true);//if the user start mooving the snimation change
        }
        else
        {
            animator.SetBool("inRunning",false);//is stop moving change the animation
        }
        miRigidbody.linearVelocity = new Vector2(inputMovimiento * Speed, miRigidbody.linearVelocity.y);
        GestionarOrientacion(inputMovimiento);//make a verctor and get "inputmovimiento"*"Velocity"

    }


    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;//the character rebound
            miRigidbody.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);//add a force rebound
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;//elimate damage
    }

    //turns the character when walking in the opposite predefined direction (from right to left)
    void GestionarOrientacion(float inputMovimiento)
    {
        //if the condition is met
        if ((MirandoDerecha && inputMovimiento < 0) || (!MirandoDerecha && inputMovimiento > 0))
        {
            //execute spin code
            MirandoDerecha = !MirandoDerecha;

            Vector3 escalaActual = transform.localScale;
            escalaActual.x *= -1;
            transform.localScale = escalaActual;
        }
    }



}
