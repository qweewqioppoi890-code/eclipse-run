using UnityEngine;

/// <summary>
/// Attach to the same GameObject as RunnerController.
/// Swipe left/right to change lanes and swipe up to jump.
/// </summary>
[RequireComponent(typeof(RunnerController))]
public class MobileSwipeInput : MonoBehaviour
{
    [SerializeField, Min(10f)] private float swipeThresholdPixels = 60f;

    private RunnerController runner;
    private Vector2 touchStart;
    private bool trackingTouch;

    private void Awake()
    {
        runner = GetComponent<RunnerController>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            trackingTouch = false;
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStart = touch.position;
            trackingTouch = true;
            return;
        }

        if (!trackingTouch || touch.phase != TouchPhase.Ended)
            return;

        trackingTouch = false;
        Vector2 delta = touch.position - touchStart;

        if (delta.magnitude < swipeThresholdPixels)
            return;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x < 0f)
                runner.MoveLeft();
            else
                runner.MoveRight();
        }
        else if (delta.y > 0f)
        {
            runner.Jump();
        }
    }
}
