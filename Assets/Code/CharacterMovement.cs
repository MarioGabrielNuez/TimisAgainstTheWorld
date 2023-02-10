using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float fuerzaSalto = 1.0f;
    public bool puedoSaltar;

    public bool estoyAtacando;
    public bool avanzoSolo;
    public float impulsoDeGolpe = 10f;
    public bool saltito;

    void Start()
    {
        puedoSaltar = true;
        anim = GetComponent<Animator>();
        estoyAtacando = false;
        saltito = false;
    }
    void FixedUpdate() {
        if (!estoyAtacando) {
            transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
            transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);
        }
        if (avanzoSolo) {
            rb.velocity = transform.forward * impulsoDeGolpe;
        }
    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
         if (Input.GetKeyDown(KeyCode.Return) && !estoyAtacando) // && !estoyAtacando)
        {
            anim.SetBool("golpeo",true);
            estoyAtacando = true;
        }
        else {
            //anim.SetBool("golpeo",false);
            estoyAtacando = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !saltito) {
            anim.SetBool("saltar", true);
            saltito= true;
        }
        else
        {
            saltito = false;
        }
        anim.SetFloat("VelX",x);
        anim.SetFloat("VelY",y);
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
    public void dejarSaltar() {
        anim.SetBool("saltar",false);
    }
    public void estoyCallendo() {
        anim.SetBool("salte", false);
    }
    public void dejeDeGolpear() {
        estoyAtacando = false;
        avanzoSolo = false;
        anim.SetBool("golpeo", false);
    }
    public void avanzarSolo() {
        avanzoSolo = true;
    }
    public void DejoDeAvanzar() {
        avanzoSolo = false;
    }
}
