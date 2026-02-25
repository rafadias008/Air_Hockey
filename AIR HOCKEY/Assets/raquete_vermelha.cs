// using UnityEngine;

// public class raquete_vermelha : MonoBehaviour
// {   
//     public KeyCode moveUp = KeyCode.UpArrow;      // Move a raquete para cima
//     public KeyCode moveDown = KeyCode.DownArrow;    // Move a raquete para baixo
//     public KeyCode moveLeft = KeyCode.LeftArrow;      // Move a raquete para esquerda
//     public KeyCode moveRight = KeyCode.RightArrow;    // Move a raquete para direita
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
//         if (pos.y > -1.0f) {                  
//             pos.y = -1.0f;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
//         }
//         else if (pos.y < -6.8f) {
//             pos.y = -6.8f;                    // Corrige a posicao da raquete caso ele ultrapasse o limite inferior
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

public class raquete_vermelha : MonoBehaviour
{
    public float speed = 10.0f;

    public float boundYMax = -1.0f;
    public float boundYMin = -6.8f;
    public float boundXMax = 3.8f;
    public float boundXMin = -3.8f;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPos = new Vector2(mousePos.x, mousePos.y);

        targetPos.x = Mathf.Clamp(targetPos.x, boundXMin, boundXMax);
        targetPos.y = Mathf.Clamp(targetPos.y, boundYMin, boundYMax);

        // Move rápido em direção ao mouse (responsivo)
        Vector2 newPos = Vector2.MoveTowards(rb2d.position, targetPos, speed * Time.fixedDeltaTime);
        rb2d.MovePosition(newPos);
    }
}