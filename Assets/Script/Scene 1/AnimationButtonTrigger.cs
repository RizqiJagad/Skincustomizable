using UnityEngine;

public class AnimationButtonTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void TriggerActionAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("PlayAction");
        }
    }
}
