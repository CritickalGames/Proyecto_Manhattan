using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    private Player playerScript;

    void Start()
    {
        playerScript = GetComponent<Player>();
    }
    public void TestAtack()
    {
        Debug.Log("TestWorking");
    }
    public void TestSpecialAtack()
    {
        Debug.Log("Test2Working");
    }
}
