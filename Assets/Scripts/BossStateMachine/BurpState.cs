using System.Collections;
using UnityEngine;

public class BurpState : State
{
    private bool canActivateCloud = true; // Controla si la nube puede activarse
    private float cloudDuration = 5f;     // Duración de la nube tóxica activa
    private float cloudCooldown = 10f;    // Cooldown antes de volver a activar la nube

    public BurpState(BossController boss) : base(boss) {}

    public override void Entry()
    {
        base.Entry();
        Debug.Log("Burp State Entered");

        // Intentamos activar la nube si es posible
        if (canActivateCloud)
        {
            Boss.StartCoroutine(ActivateToxicCloud());
        }
    }

    public override void Exit()
    {
        // Aquí podrías manejar la salida del estado (detener animaciones, etc.)
        base.Exit();
        Debug.Log("Exiting Burp State");
    }

    public override void Update()
    {
        // Lógica adicional si el jefe necesita realizar alguna acción en este estado
    }

    private IEnumerator ActivateToxicCloud()
    {
        canActivateCloud = false;  // Evitar que se active antes de que termine el cooldown

        // Activar la nube tóxica
        Boss.ActivateToxicArea(true);
        Debug.Log("Nube tóxica activada.");

        // Esperar durante la duración de la nube
        yield return new WaitForSeconds(cloudDuration);

        // Desactivar la nube tóxica
        Boss.ActivateToxicArea(false);
        Debug.Log("Nube tóxica desactivada.");

        // Esperar el tiempo de cooldown antes de poder activarla nuevamente
        yield return new WaitForSeconds(cloudCooldown);

        // Permitir que la nube se active de nuevo después del cooldown
        canActivateCloud = true;
        Debug.Log("Nube tóxica lista para reactivarse.");

        // Cambiar al estado de recuperación
        Boss.ChangeStateKey(States.Recovery);
    }
}
