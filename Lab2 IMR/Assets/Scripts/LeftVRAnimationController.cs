using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeftVRAnimationController : MonoBehaviour
{
    private ActionBasedController leftHandController;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        GameObject leftControllerObject = GameObject.Find("Left Controller");
        if (leftControllerObject)
        {
            leftHandController = leftControllerObject.GetComponent<ActionBasedController>();
        }
    }

    private void Update()
    {
        HandleInput(leftHandController);
    }

    private void HandleInput(ActionBasedController controller)
    {
        if (controller)
        {
            bool isGrabbing = controller.selectAction.action.ReadValue<float>() > 0.0f; // Assuming a threshold
            bool isTriggering = controller.activateAction.action.ReadValue<float>() > 0.0f; // Assuming a threshold
            Debug.Log(controller.selectAction.action.ReadValue<float>());
            animator.SetBool("IsLeftGrabbing", isGrabbing);
            animator.SetBool("IsLeftTriggering", isTriggering);
        }
    }
}
