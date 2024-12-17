using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageState : State
{
    public RageState(BossController Boss) : base(Boss) {}

    public override void Entry()
    {
        base.Entry();
        Debug.Log("Rage State Entered");

        // Cambiar el color o algún efecto visual para indicar que el jefe está en furia
        Boss.GetComponent<SpriteRenderer>().color = Color.red; // Cambiar color a rojo como ejemplo

        // Cambiar al estado Burp
        Boss.ChangeStateKey(States.Burp); // Activar BurpState cuando entra en Rage
    }


}
