using UnityEngine;

public class Melee : MonoBehaviour
{
    [System.NonSerialized]public MeleeMovement movementScript;
    [System.NonSerialized]public MeleeAttack attackScript;
    [SerializeField] private float followRange;
    [SerializeField] private GameObject followObject;
    private bool attacking;
    private GameObject target;
    void Start()
    {
        movementScript = GetComponent<MeleeMovement>();
        attackScript = GetComponent<MeleeAttack>();
        target = GameObject.Find("/Player/Player");;
    }
    void Update()
    {
        DetectPlayer();
    }
    void DetectPlayer()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) < followRange && !attacking)
        {
            if ((transform.position.x - target.transform.position.x) < 0)
                movementScript.direction = -1;
            if ((transform.position.x - target.transform.position.x) > 0)
                movementScript.direction = 1;
            movementScript.FollowPlayer();
        }
    }
}
