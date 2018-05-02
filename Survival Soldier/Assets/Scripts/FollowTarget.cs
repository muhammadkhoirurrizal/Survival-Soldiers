using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public GameObject Target;
    private Vector3 TargetPos;

    [SerializeField]
    float MoveSpeed;

    [SerializeField]
    BoxCollider2D BoundBox;
    private Vector3 MinBounds, MaxBounds;

    private Camera TheCamera;
    private float HalfHeight, HalfWeidht;

    float Flip;

    public static FollowTarget Instance;

	// Use this for initialization
	void Start () {
        MinBounds = BoundBox.bounds.min;
        MaxBounds = BoundBox.bounds.max;

        if (Instance == null)
            Instance = this;

        TheCamera = GetComponent<Camera>();
        HalfHeight = TheCamera.orthographicSize;
        HalfWeidht = HalfHeight * Screen.width / Screen.height;
	}

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            TargetPos = new Vector3(Target.transform.position.x + (Target.transform.GetChild(0).transform.localScale.x * 10), Target.transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, TargetPos, MoveSpeed * Time.deltaTime);

            float clampeX = Mathf.Clamp(transform.position.x, MinBounds.x + HalfWeidht, MaxBounds.x - HalfWeidht);
            float clampeY = Mathf.Clamp(transform.position.y, MinBounds.y + HalfHeight, MaxBounds.y - HalfHeight);
            transform.position = new Vector3(clampeX, clampeY, transform.position.z);
        }
    }
}
    