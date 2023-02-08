using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerHealth))]
public class RollerPlayer : MonoBehaviour
{
    private int score = 0;

    private Vector3 force;
    private Vector3 torqueForce;
    private Rigidbody RigBod;
    private bool onGround = false;
    [SerializeField] private Transform cam;
    [SerializeField] private float MaxForce = 5;
    [SerializeField] private float JumpForce = 50;
    [SerializeField] private float airForceMultiplier = 1.5f;


    [SerializeField] private float groundRayLength = 1;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        RigBod = GetComponent<Rigidbody>();
        RollerGameManager.Instance.SetHealth((int)GetComponent<PlayerHealth>().MaxHealth);
        RollerGameManager.Instance.SetScore(0);

        cam = Camera.main.transform;
        Camera.main.GetComponent<RollCam>().setTarget(this.transform);

        GetComponent<PlayerHealth>().onDamage += onDamage;
        GetComponent<PlayerHealth>().onHeal += onHeal;
        GetComponent<PlayerHealth>().onDeath += onDeath;

    }

    // Update is called once per frame

    private void Update()
    {
        Vector3 dir = Vector3.zero;
        Vector3 torqDir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        torqDir.x = Input.GetAxis("Vertical");
        torqDir.z = Input.GetAxis("Horizontal") * -1;

        Quaternion viewSpace = Quaternion.AngleAxis(cam.rotation.eulerAngles.y, Vector3.up);

        force = viewSpace * dir * MaxForce * airForceMultiplier;
        torqueForce = (viewSpace * torqDir).normalized * MaxForce * Time.deltaTime * 100;
        //force = dir * MaxForce;

        Ray ray = new Ray(transform.position, Vector3.down);
        onGround = Physics.Raycast(ray, groundRayLength, groundLayer);
        //Debug.DrawRay(transform.position, ray.direction * groundRayLength);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            RigBod.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
        
    }

    private void FixedUpdate()
    {
        if(!onGround) RigBod.AddForce(force);
        RigBod.AddTorque(torqueForce, ForceMode.Force);
    }

    public void AddPuntos(int points)
    {
        score += points;
        RollerGameManager.Instance.SetScore(score);
        Debug.Log(points);
    }

    public void onDamage()
    {
        RollerGameManager.Instance.SetHealth((int)GetComponent<PlayerHealth>().health);
    }

    public void onHeal()
    {
        RollerGameManager.Instance.SetHealth((int)GetComponent<PlayerHealth>().health);
    }

    public void onDeath()
    {
        RollerGameManager.Instance.setGameOver();
        Destroy(gameObject);
    }
}
