using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed = 50f;
    public float LerpStep = 0.1f;
    public float JumpPower = 150f;
    public bool Grounded;
    [SerializeField]
    float PlayerHealth = 100;

    float Flip;
    bool Fliped;
    public GameObject PlayerModel;

    public Transform GroundCheck;
    public LayerMask GroundLayer;

    public GameObject Lighting;


    public GameObject CurrenGun;
    public Transform PosGun;

    Vector2 newPos;

    Rigidbody2D MyRb;
    Animator MyAnim;

    // Use this for initialization
    void Start()
    {
        MyRb = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();
        Flip = PlayerModel.transform.localScale.x;
    }

    private void Update()
    {
        MyAnim.SetBool("Grounded", Grounded);
        MyAnim.SetFloat("Walk", Mathf.Abs(Input.GetAxis("Horizontal")));

        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.15f, GroundLayer);

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            Fliped = false;
            PlayerModel.transform.localScale = new Vector3(Flip, PlayerModel.transform.localScale.y, PlayerModel.transform.localScale.z);
        }
        else if (Input.GetAxis("Horizontal") < -0.1f)
        {
            Fliped = true;
            PlayerModel.transform.localScale = new Vector3(-Flip, PlayerModel.transform.localScale.y, PlayerModel.transform.localScale.z);
        }

        if (CurrenGun != null && CurrenGun.transform.localScale != PosGun.transform.localScale)
        {
            CurrenGun.transform.localScale = PosGun.transform.localScale;
        }

        if (CurrenGun != null && CurrenGun.GetComponentInChildren<Light>().enabled == false)
        {
            CurrenGun.GetComponentInChildren<Light>().enabled = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Controller();

    }

    void Controller()
    {
        float H = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * (H * Speed) * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && Grounded)
        {
            MyRb.AddForce(Vector2.up * JumpPower);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Destroy(CurrenGun.gameObject);
            CurrenGun = null;
        }

        if (CurrenGun != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                CurrenGun.GetComponent<Weapon>().Shoot(PlayerModel.transform.localScale.x);
            }
        }
        else if (CurrenGun == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MyAnim.SetTrigger("IsAttack");
            }
        }
    }

    public void PlayerGetDamage(int Damage)
    {
        PlayerHealth -= Damage;
        if (PlayerHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slice")
        {
            PlayerGetDamage(5);
        }
    }
}
