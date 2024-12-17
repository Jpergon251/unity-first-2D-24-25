using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject toxicCloud;
    [SerializeField] private PlayerController Player;
    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] private Transform firePoint; // Punto desde donde dispara el jefe

   
    public float currentHealth;
    private float maxHealth = 200;
    
    State currentState;
    Dictionary<States, State> statesDict = new Dictionary<States, State>();

    // ready
    void Start() 
    {
        // inicializar datos boss
        currentHealth = maxHealth;
        
        // inicializar estados:
        //      definir estado inicial
        currentState = new FollowState(this);
        currentState.Entry();
        //      crear lista de estados
        statesDict.Add(States.Follow, currentState);
        statesDict.Add(States.Rage, new RageState(this));
        statesDict.Add(States.Spit, new SpitState(this));
        statesDict.Add(States.Burp, new BurpState(this));
        statesDict.Add(States.Recovery, new RecoveryState(this));
        
        //      preparar sistema de eventos
    }

    // process
    void Update()
    {
        // llamar update del estado actual
        currentState.Update();
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public void ChangeStateKey(States newState)
    {
        if(statesDict.ContainsKey(newState))
        {
            ChangeState(statesDict[newState]);
        }
        else
        {
            Debug.LogWarning("State not in list.");
        }
    }
    
    void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Entry();
    }    

     // Método para obtener la posición del jugador
    public Vector2 GetPlayerPosition()
    {
        return Player.transform.position;
    }

    // Método para obtener el prefab del proyectil
    public GameObject GetProjectilePrefab()
    {
        return projectilePrefab;
    }

    // Método para obtener el punto de disparo
    public Transform GetFirePoint()
    {
        return firePoint;
    }

    // Método para activar la nube tóxica
    public void ActivateToxicArea(bool isActive)
{
    if (toxicCloud != null)
    {
        toxicCloud.SetActive(isActive);
    }
    else
    {
        Debug.LogWarning("Toxic Cloud object not assigned!");
    }
}
}

public enum States
{
    Follow, Spit, Burp, Recovery, Rage,
}