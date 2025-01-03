using UnityEngine;

public class CameraControler : MonoBehaviour
{

    public Transform Personaje;

    private float tamanoCamara;
    private float alturaPantalla;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tamanoCamara = Camera.main.orthographicSize;
        alturaPantalla = tamanoCamara * 2;
    }

    // Update is called once per frame
    void Update()
    {
        CalcularPosicionCamara();
    }

    void CalcularPosicionCamara()
    {
        int pantallaPersonaje = (int)(Personaje.position.y / alturaPantalla);
        float alturaCamara = (pantallaPersonaje * alturaPantalla) + tamanoCamara;

        transform.position = new Vector3(transform.position.x,alturaCamara,transform.position.z);
    }

}
