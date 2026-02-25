using UnityEngine;
using System.Collections;

public class SideWalls : MonoBehaviour
{
    // Verifica colisões da bola nas paredes
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Finish")
        {
            string wallName = transform.name;
            GameManager.Score(wallName);
            hitInfo.gameObject.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
