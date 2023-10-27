using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RightVRAnimationController : MonoBehaviour
{
    private ActionBasedController rightHandController;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        GameObject rightControllerObject = GameObject.Find("Right Controller");
        if (rightControllerObject)
        {
            rightHandController = rightControllerObject.GetComponent<ActionBasedController>();
        }
    }

    private void Update()
    {
        HandleInput(rightHandController);
    }

    private void HandleInput(ActionBasedController controller)
    {
        if (controller)
        {
            bool isGrabbing = controller.selectAction.action.ReadValue<float>() > 0.0f; // Assuming a threshold
            bool isTriggering = controller.activateAction.action.ReadValue<float>() > 0.0f; // Assuming a threshold
            Debug.Log(controller.selectAction.action.ReadValue<float>());
            animator.SetBool("IsRightGrabbing", isGrabbing);
            animator.SetBool("IsRightTriggering", isTriggering);
        }
    }
}
