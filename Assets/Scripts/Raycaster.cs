using UnityEngine;
using System.Collections;

public class Raycaster : MonoBehaviour
{

    [SerializeField]
    private Vector2 direction;
    private AI mainAI;
    private bool isWall;

    // Use this for initialization
    void Start()
    {
        mainAI = AI.Instance;
        mainAI.AddSensor(this);
    }

    // Update is called once per frame
    void Update()
    {
        ShotRay();
    }

    public void ShotRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out hit, 0.35f))
        {
            if (hit.transform.gameObject.CompareTag("Wall"))
            {
                isWall = true;
            }
        }
        else
        {
            isWall = false;
        }
        Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 0.3f, Color.red);
    }

    public bool IsWall
    {
        get
        {
            return isWall;
        }
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }
    }
}
