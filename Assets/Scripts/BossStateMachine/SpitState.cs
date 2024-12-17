using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitState : State
{
    private float spitCooldown = 2f; // Tiempo entre escupitajos
    private bool isSpitting = false; // Para evitar múltiples llamadas

    public SpitState(BossController Boss) : base(Boss) {}

    public override void Entry()
    {
        base.Entry();
        Debug.Log("Spit State Entered");

        if (!isSpitting)
        {
            Boss.StartCoroutine(Spit());
        }
    }

    private IEnumerator Spit()
    {
        isSpitting = true;

        // Instanciar proyectil
        GameObject projectilePrefab = Boss.GetProjectilePrefab();
        Transform firePoint = Boss.GetFirePoint();

        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = GameObject.Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Direccionar el proyectil hacia el jugador
            Vector2 direction = (Boss.GetPlayerPosition() - (Vector2)firePoint.position).normalized;
            projectile.GetComponent<Projectile>().SetDirection(direction);
        }

        yield return new WaitForSeconds(spitCooldown);

        isSpitting = false;

        // Cambiar al estado de recuperación
        Boss.ChangeStateKey(States.Recovery);
    }
}
