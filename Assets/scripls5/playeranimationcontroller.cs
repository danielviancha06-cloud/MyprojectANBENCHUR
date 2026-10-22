using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class playeranimationcontroller : MonoBehaviour
{
    public Animation animator;


    [SerializeField] private InputActionAsset inputActionsMap;

    private InputAction inputAction;

    private void OnEnable()
    {
        inputActionsMap.FindActionMap("payer").Enable();

    }

    private void OnDisable()

    {
        inputActionsMap.FindActionMap("payer").Disable();


    }

    private void Awake()

    {
        axisAction = inputActionsMap.FindAction("move");
    }



    void opdate()
    {
        Vector2 speed = axisAction.readValue<Vector2>();

        float horisontalAxis = speed.x;
        float verticaAxis = speed.y;

        if (horisontalAxis != 0)
        {
            animator.setBool("iswalk", true);

        }
        else
        {
            animator.setBool("iswalk", true);
        }

    }
