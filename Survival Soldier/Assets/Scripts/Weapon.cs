using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField]
    float RangeBullet,DamageGiven,PowerShoot,DelayShoot;

    float CurrentDelay;

    [SerializeField]
    GameObject BulletPref,PosShoot;

	// Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<Light>().intensity = RangeBullet + 2.1f;
        CurrentDelay = DelayShoot;
	}
	
	// Update is called once per frame
	void Update () {
        if (CurrentDelay > 0)
        {
            CurrentDelay -= 1 * Time.deltaTime;
        }
    }

    public void Shoot(float Direction)
    {
        if (CurrentDelay <= 0)
        {
            //GameObject BulletClone = PhotonNetwork.Instantiate(BulletPref, PosShoot.transform.position, Quaternion.identity);
            GameObject BulletClone = Instantiate(BulletPref, PosShoot.transform.position, Quaternion.identity);
            BulletClone.GetComponent<Bullet>().Damage = (int)DamageGiven;
            BulletClone.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (Direction * PowerShoot));
            Destroy(BulletClone, RangeBullet);
            CurrentDelay = DelayShoot;
        }
    }
}
