// using UnityEngine;

// public class raquete_azul : MonoBehaviour
// {   
//     public KeyCode moveUp = KeyCode.W;      // Move a raquete para cima
//     public KeyCode moveDown = KeyCode.S;    // Move a raquete para baixo
//     public KeyCode moveLeft = KeyCode.A;      // Move a raquete para esquerda
//     public KeyCode moveRight = KeyCode.D;    // Move a raquete para direita
//     public float speed = 5.0f;             // Define a velocidade da raquete
//     public float boundY = 20.0f;            // Define os limites em Y
//     public float boundX = 3.8f;            // Define os limites em X
//     private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         rb2d = GetComponent<Rigidbody2D>();     // Inicializa a raquete

//     }

//     // Update is called once per frame
//     void Update(){
//         var vel = rb2d.linearVelocity;                // Acessa a velocidade da raquete
//         if (Input.GetKey(moveUp)) {             // Velocidade da Raquete para ir para cima
//             vel.y = speed;
//         }
//         else if (Input.GetKey(moveDown)) {      // Velocidade da Raquete para ir para baixo
//             vel.y = -speed;                    
//         }
//         else if (Input.GetKey(moveLeft)) {             // Velocidade da Raquete para ir para cima
//             vel.x = -speed;
//         }
//         else if (Input.GetKey(moveRight)) {             // Velocidade da Raquete para ir para cima
//             vel.x = speed;
//         }
//         else {
//             vel.y = 0; 
//             vel.x = 0;                          // Velociade para manter a raquete parada
//         }
//         rb2d.linearVelocity = vel;                    // Atualizada a velocidade da raquete

//         var pos = transform.position;           // Acessa a Posição da raquete
//         if (pos.y > 6.8f) {                  
//             pos.y = 6.8f;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
//         }
//         else if (pos.y < 1.0f) {
//             pos.y = 1.0f;                    // Corrige a posicao da raquete caso ele ultrapasse o limite inferior
//         }
//         else if (pos.x > 3.8f) {
//             pos.x = 3.8f;                     // Corrige a posicao da raquete caso ele ultrapasse o limite direito
//         }
//         else if (pos.x < -3.8f) {
//             pos.x = -3.8f;                    // Corrige a posicao da raquete caso ele ultrapasse o limite esquerdo
//         }
//         transform.position = pos;               // Atualiza a posição da raquete

//         print(pos);                             // Imprime a posição da raquete no console
//     }
  
// }  

using UnityEngine;

public class raquete_azul : MonoBehaviour
{
    public float speed = 4.5f;
    public float deadZone = 0.20f;

    public Transform ball;

    public float reactionTime = 0.20f;       // quanto maior, mais lerda a reação
    public float aimError = 0.3f;            // erro em unidades do mundo (0.3 a 1.2 é bom)
    public float errorChangeTime = 0.25f;    // de quanto em quanto tempo muda o erro

    private Rigidbody2D rb2d;

    private float nextThinkTime = 0f;
    private Vector2 desiredVel;
    private Vector2 currentError;
    private float nextErrorTime = 0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (ball == null)
        {
            GameObject b = GameObject.FindGameObjectWithTag("Finish");
            if (b != null) ball = b.transform;
        }
    }

    void Update()
    {
        if (ball == null)
        {
            rb2d.linearVelocity = Vector2.zero;
            return;
        }

        // Atualiza erro de mira periodicamente
        if (Time.time >= nextErrorTime)
        {
            currentError = new Vector2(
                Random.Range(-aimError, aimError),
                Random.Range(-aimError, aimError)
            );
            nextErrorTime = Time.time + errorChangeTime;
        }

        // “Pensa” só de tempos em tempos (reação atrasada)
        if (Time.time >= nextThinkTime)
        {
            Vector2 target = (Vector2)ball.position + currentError;

            float dx = target.x - (float)transform.position.x;
            float dy = target.y - (float)transform.position.y;

            float vx = 0f;
            float vy = 0f;

            if (dx > deadZone) vx = speed;
            else if (dx < -deadZone) vx = -speed;

            if (dy > deadZone) vy = speed;
            else if (dy < -deadZone) vy = -speed;

            desiredVel = new Vector2(vx, vy);
            nextThinkTime = Time.time + reactionTime;
        }

        rb2d.linearVelocity = desiredVel;

        // Seus limites
        var pos = transform.position;

        if (pos.y > 6.8f) pos.y = 6.8f;
        else if (pos.y < 1.0f) pos.y = 1.0f;

        if (pos.x > 3.8f) pos.x = 3.8f;
        else if (pos.x < -3.8f) pos.x = -3.8f;

        transform.position = pos;
    }
}