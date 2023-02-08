using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1, 30), Tooltip("Controlls how quickly the player moves")] public float moveSpeed = 5;
    public Transform bulletOrigin;
    public float rotationSpeed = 75f;
    public GameObject BulletPrefab;
    private void Awake()
    {
        Debug.Log("I get up!");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        //transform.localScale = Vector3.one * Random.value * 5;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(2, 3, 2);
        //transform.rotation = Quaternion.Euler(30, 30, 30);
        Vector3 direction = Vector3.zero;
        direction.z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Horizontal");

        Quaternion Rotate = Quaternion.Euler(rotation * rotationSpeed * Time.deltaTime);
        transform.rotation = transform.rotation * Rotate;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        /*if (Input.GetKey(KeyCode.W)) direction.z += moveSpeed;
        if (Input.GetKey(KeyCode.A)) direction.x -= moveSpeed;
        if (Input.GetKey(KeyCode.S)) direction.z -= moveSpeed;
        if (Input.GetKey(KeyCode.D)) direction.x += moveSpeed;

        transform.position += direction * moveSpeed * Time.deltaTime;*/

        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log("BANG");
            //GetComponent<AudioSource>().Play();
            var go = Instantiate(BulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
            Destroy(go, 5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I HIT " + other);
        if(other.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AsteroidGameManager>()?.SetGameOver();
        }
    }
}
