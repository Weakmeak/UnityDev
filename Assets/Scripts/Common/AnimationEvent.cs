using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    void onPlayEffect(GameObject go)
    {
        Instantiate(go); 
    }
}
