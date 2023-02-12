using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPies : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterMovement logicaPersonaje1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay( Collider other) {
        logicaPersonaje1.puedoSaltar = true;
    }
    private void OnTriggerExit(Collider other)
    {
        logicaPersonaje1.puedoSaltar = false;
    }
}
