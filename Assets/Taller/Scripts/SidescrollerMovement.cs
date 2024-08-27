using UnityEngine;

public class SidescrollerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;              // Velocidad de movimiento del personaje
    public string animatorParamName = "IsMoving"; // Nombre del parámetro en el Animator

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movement;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale; // Guarda la escala original del personaje
    }

    void Update()
    {
        // Captura la entrada del usuario para el movimiento en X y Z
        float moveX = Input.GetAxis("Horizontal"); // Input de teclado o controlador
        float moveZ = Input.GetAxis("Vertical");   // Input de teclado o controlador

        // Crea el vector de movimiento basado en las entradas
        movement = new Vector3(moveX, 0f, moveZ).normalized;

        // Actualiza el valor del parámetro en el Animator
        if (movement.magnitude > 0.1f)
        {
            animator.SetFloat(animatorParamName, 1f); // Se mueve
        }
        else
        {
            animator.SetFloat(animatorParamName, 0f); // Se detiene
        }

        // Ajusta la escala del personaje según la dirección en X y Z
        Vector3 newScale = originalScale; // Inicia con la escala original

        if (moveX != 0)
        {
            newScale.x = Mathf.Sign(moveX) * Mathf.Abs(originalScale.x); // Ajusta la escala en X
        }

        if (moveZ != 0)
        {
            newScale.z = Mathf.Sign(moveZ) * Mathf.Abs(originalScale.z); // Ajusta la escala en Z
        }

        transform.localScale = newScale; // Aplica la nueva escala
    }

    void FixedUpdate()
    {
        // Aplica la fuerza para mover el Rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
