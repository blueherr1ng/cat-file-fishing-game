using UnityEngine;

public class CatController : MonoBehaviour
{
    public Animator animator;

    public void TestFn()
    {
        Debug.Log("this is a test");
    }

    public void StartFishing()
    {
        animator.SetTrigger("StartFishing");
    }

    public void CatchFish()
    {
        animator.SetTrigger("CatchFish");
    }
}
