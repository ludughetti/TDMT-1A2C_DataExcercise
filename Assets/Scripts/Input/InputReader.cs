using Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string moveActionName = "Move";
    [SerializeField] private string runActionName = "Run";
    [SerializeField] private Vector2EventChannel moveEventChannel;
    [SerializeField] private BoolEventChannel runEventChannel;

    private void OnEnable()
    {
        var moveAction = inputActions.FindAction(moveActionName);
        if (moveAction != null)
        {
            moveAction.performed += HandleMoveInput;
            moveAction.canceled += HandleMoveInput;
        }
        var runAction = inputActions.FindAction(runActionName);
        if (runAction != null)
        {
            runAction.started += HandleRunInputStarted;
            runAction.canceled += HandleRunInputCanceled;
        }
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        //TODO: [Done] Implement event logic
        if(moveEventChannel != null) 
            moveEventChannel.Invoke(ctx.ReadValue<Vector2>());
    }

    private void HandleRunInputStarted(InputAction.CallbackContext ctx)
    {
        //TODO: [Done] Implement event logic
        Debug.Log($"{name}: Run input started");
        if(runEventChannel != null)
            runEventChannel.Invoke(true);
    }

    private void HandleRunInputCanceled(InputAction.CallbackContext ctx)
    {
        //TODO: [Done] Implement event logic
        Debug.Log($"{name}: Run input canceled");
        if (runEventChannel != null)
            runEventChannel.Invoke(false);
    }
}
