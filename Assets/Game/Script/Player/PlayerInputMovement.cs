using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;

using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerInputMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick Joystick;

    [SerializeField] private Vector2 JoystickSize = new Vector2(300, 300);
    [SerializeField] private bool DynamicJoystick;

    NavMeshAgent Agent;
    PlayerAnimator Animator;

    private Finger MovementFinger;
    private Vector2 Direction;
    private float joystickRadius;


    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<PlayerAnimator>();

        EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += HandleFingerDown;
        EnhancedTouch.Touch.onFingerUp += HandleLoseFinger;
        EnhancedTouch.Touch.onFingerMove += HandleFingerMove;

        joystickRadius = JoystickSize.x / 2;
    }

    private void OnDestroy()
    {
        EnhancedTouch.Touch.onFingerDown -= HandleFingerDown;
        EnhancedTouch.Touch.onFingerUp -= HandleLoseFinger;
        EnhancedTouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    Vector2 knobPosition;
    float maxMovement;
    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            maxMovement = JoystickSize.x * 0.35f;
            EnhancedTouch.Touch currentTouch = MovedFinger.currentTouch;

            if (DynamicJoystick)
            {
                if (Vector2.Distance(currentTouch.screenPosition, Joystick.RectTransform.anchoredPosition) > maxMovement)
                {
                    knobPosition = (currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition).normalized * maxMovement;
                }
                else
                {
                    knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
                }

            }
            else
            {
                knobPosition = (currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition).normalized * maxMovement;
            }

            Direction = knobPosition / maxMovement;
            Joystick.Knob.anchoredPosition = knobPosition;
        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            Direction = Vector2.zero;
            Animator.SetAnimation(ANIMATION.IDLE);
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null && TouchedFinger.screenPosition.y <= Screen.height * 0.75f /* TouchedFinger.screenPosition.x <= Screen.width  / 2f */)
        {
            MovementFinger = TouchedFinger;
            Direction = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.sizeDelta = JoystickSize;
            Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
            Animator.SetAnimation(ANIMATION.RUN);
        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {

        if (StartPosition.x < joystickRadius) StartPosition.x = joystickRadius;
        else if (StartPosition.x > Screen.width - joystickRadius)
        {
            StartPosition.x = Screen.width - joystickRadius;
        }


        if (StartPosition.y < joystickRadius)
        {
            StartPosition.y = joystickRadius;
        }
        // else
        // if (StartPosition.y > Screen.height - joystickRadius)
        // {
        //     StartPosition.y = Screen.height - joystickRadius;
        // }

        return StartPosition;
    }

    Vector3 scaledMovement;
    private void Update()
    {
        if (MovementFinger == null) return;
        scaledMovement = Agent.speed * Time.deltaTime * new Vector3(
            Direction.x,
            0,
            Direction.y
        );

        Agent.transform.LookAt(Agent.transform.position + scaledMovement, Vector3.up);
        Agent.Move(scaledMovement);
    }

    private void OnGUI()
    {
        GUIStyle labelStyle = new GUIStyle()
        {
            fontSize = 24,
            normal = new GUIStyleState()
            {
                textColor = Color.white
            }
        };
        if (MovementFinger != null)
        {
            GUI.Label(new Rect(10, 35, 500, 20), $"Finger Start Position: {MovementFinger.currentTouch.startScreenPosition}", labelStyle);
            GUI.Label(new Rect(10, 65, 500, 20), $"Finger Current Position: {MovementFinger.currentTouch.screenPosition}", labelStyle);
        }
        else
        {
            GUI.Label(new Rect(10, 35, 500, 20), "No Current Movement Touch", labelStyle);
        }

        GUI.Label(new Rect(10, 10, 500, 20), $"Screen Size ({Screen.width}, {Screen.height})", labelStyle);
    }
}
