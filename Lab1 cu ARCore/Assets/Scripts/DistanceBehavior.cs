using UnityEngine;

public class DistanceBehavior : MonoBehaviour
{
    public string targetTag = "Attackable";
    public float attackRange = 0.2f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        bool withinRange = false;

        foreach (var target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= attackRange)
            {
                withinRange = true;
                break;
            }
        }

        animator.SetBool("isWithinAttackRange", withinRange);
    }
}
