using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class Coin : Interactable
{
    //[SerializeField] GameObject pickupFX; 
    [SerializeField] int pointval; 

    // Start is called before the first frame update
    void Start()
    {
        //onEnter += OnCoinPickup;
        GetComponent<CollisionEvent>().onEnter += OnInteract;
    }

    override public void OnInteract(GameObject go)
    {
        if (go.TryGetComponent<RollerPlayer>(out RollerPlayer player)) player.AddPuntos(pointval); 

        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        if(interactFX!= null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (destroyOnInteract) Destroy(this.gameObject);
    }
}
