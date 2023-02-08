using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class HeatlhPickup : Interactable
{
    [SerializeField] private float healthValue = 50;
    public override void OnInteract(GameObject target)
    {
       if (target.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            health.OnApplyHealth(healthValue);
            if(destroyOnInteract) Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
    }
}
