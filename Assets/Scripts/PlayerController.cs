using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    public float sensibilidad = 100f;
    public Animator animator;
    public Transform mirilla;

    private PlayerInputActions inputActions;
    private CharacterController characterController;
    private Vector2 inputMovimiento;
    private Vector2 inputRaton;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Look.canceled += OnLook;
        inputActions.Player.Fire.performed += OnFire;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        inputMovimiento = context.ReadValue<Vector2>();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        inputRaton = context.ReadValue<Vector2>();
    }

    void OnFire(InputAction.CallbackContext context)
    {
        Disparar();
    }

    void Disparar()
    {
        if (mirilla != null)
        {
            if (inputMovimiento.magnitude < 0.1f && animator != null)
            {
                animator.SetTrigger("Shoot");
            }

            GameObject bullet = BulletPool.Instance.GetBullet();
            bullet.transform.position = mirilla.position;
            bullet.transform.rotation = mirilla.rotation;
        }
    }

    void Update()
    {
        Mover();
        Rotar();
        ActualizarAnimaciones();
    }

    void Mover()
    {
        Vector3 movimiento = new Vector3(-inputMovimiento.x, 0, -inputMovimiento.y);
        movimiento = transform.TransformDirection(movimiento);

        characterController.Move(movimiento * velocidad * Time.deltaTime);
        characterController.Move(Vector3.down * 9.8f * Time.deltaTime);
    }

    void Rotar()
    {
        float rotacion = inputRaton.x * sensibilidad * Time.deltaTime;
        transform.Rotate(0, rotacion, 0);
    }

    void ActualizarAnimaciones()
    {
        float speed = inputMovimiento.magnitude;

        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    }
}