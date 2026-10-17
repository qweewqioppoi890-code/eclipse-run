using UnityEngine;

/// <summary>
/// Basic three-lane runner movement. Attach to a CharacterController object.
/// The scene needs a CharacterController component on the same GameObject.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class RunnerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Min(1f)] private float forwardSpeed = 8f;
    [SerializeField, Min(1f)] private float laneChangeSpeed = 10f;
    [SerializeField] private float laneWidth = 2.2f;
    [SerializeField] private float jumpHeight = 1.8f;
    [SerializeField] private float gravity = -24f;

    private CharacterController controller;
    private int laneIndex = 1; // 0 = left, 1 = center, 2 = right
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        ReadInput();

        float targetX = (laneIndex - 1) * laneWidth;
        float xVelocity = (targetX - transform.position.x) * laneChangeSpeed;

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        if (controller.isGrounded && JumpPressed())
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 motion = new Vector3(xVelocity, verticalVelocity, forwardSpeed);
        controller.Move(motion * Time.deltaTime);
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            laneIndex = Mathf.Max(0, laneIndex - 1);

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            laneIndex = Mathf.Min(2, laneIndex + 1);
    }

    private bool JumpPressed()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.W);
    }

    public void MoveLeft() => laneIndex = Mathf.Max(0, laneIndex - 1);
    public void MoveRight() => laneIndex = Mathf.Min(2, laneIndex + 1);
    public void Jump()
    {
        if (controller.isGrounded)
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle") && GameManager.Instance != null)
            GameManager.Instance.EndRun();
    }
}