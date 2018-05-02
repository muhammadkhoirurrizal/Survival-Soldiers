using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWeapon : MonoBehaviour {

    public GameObject GunPref;

    public GameObject CurrentGun;
    public Transform PosGun;

    float WaitSpawn = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (WaitSpawn > 0)
        {
            WaitSpawn -= 1 * Time.deltaTime;
        }

        if (CurrentGun == null && WaitSpawn <= 0)
        {
            CurrentGun = Instantiate(GunPref, PosGun.position, Quaternion.identity);
            CurrentGun.transform.parent = gameObject.transform;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().CurrenGun == null)
        {
            CurrentGun.transform.parent = collision.gameObject.GetComponent<PlayerMovement>().PlayerModel.transform;
            collision.gameObject.GetComponent<PlayerMovement>().CurrenGun = CurrentGun;
            collision.gameObject.GetComponent<PlayerMovement>().CurrenGun.transform.position = collision.gameObject.GetComponent<PlayerMovement>().PosGun.position;
            CurrentGun = null;
            WaitSpawn = 5;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
