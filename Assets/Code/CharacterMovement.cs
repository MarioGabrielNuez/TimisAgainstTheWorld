using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    public float fuerzaSalto = 1.0f;
    public float impulsoDeGolpe = 10f;
    public float x, y;
    public float sensX;
    public float sensY;
    private float mouseX;
    private float mouseY;

    float xRotation;
    float yRotation;

    public CharacterController characterController;
    public Rigidbody rb;
    public Animator anim;
    public Camera camera;
    
    public Vector3 moveInput = Vector3.zero;

    public bool estoyAtacando;
    public bool avanzoSolo;
    public bool puedoSaltar;
    public bool saltito;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (!estoyAtacando)
        {
            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(0f, yRotation, 0);
            camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            moveInput = Vector3.ClampMagnitude(new Vector3(0, Input.GetButtonDown("Jump") ? fuerzaSalto : 0f, y), 1f);
            moveInput = transform.TransformDirection(moveInput) * (Input.GetMouseButton(2) ? velocidadMovimiento *= 2 : velocidadMovimiento);
            moveInput -= (-Physics.gravity + new Vector3(0f, GetComponent<Rigidbody>().mass, 0f)) * Time.deltaTime;

            characterController.Move(moveInput * Time.deltaTime);
        }

        if (avanzoSolo)
        {
            rb.velocity = transform.forward * impulsoDeGolpe;
        }
    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        if (Input.GetKeyDown(KeyCode.Return) && !estoyAtacando) // && !estoyAtacando)
        {
            anim.SetBool("golpeo", true);
            estoyAtacando = true;
        }
        else
        {
            //anim.SetBool("golpeo",false);
            estoyAtacando = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !saltito)
        {
            anim.SetBool("saltar", true);
            saltito = true;
        }
        else
        {
            saltito = false;
        }
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);
        /**
         * 
         * if (puedoSaltar) {
            anim.SetBool("pasar", true);
            if (Input.GetKeyDown(KeyCode.Space)) {
                anim.SetBool("salte",true);
                rb.AddForce(new Vector3(0,fuerzaSalto,0),ForceMode.Impulse);
            }
              anim.SetBool("salte", false);
        }
        else
        {
            estoyCallendo();
        }
        **/

    }
    public void dejarSaltar()
    {
        anim.SetBool("saltar", false);
    }
    public void estoyCallendo()
    {
        anim.SetBool("salte", false);
    }
    public void dejeDeGolpear()
    {
        estoyAtacando = false;
        avanzoSolo = false;
        anim.SetBool("golpeo", false);
    }
    public void avanzarSolo()
    {
        avanzoSolo = true;
    }
    public void DejoDeAvanzar()
    {
        avanzoSolo = false;
    }
}
