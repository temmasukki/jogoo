using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform jogador; // Arraste o objeto do jogador para este campo na Unity
    public Vector3 offset = new Vector3(0, 0, -10); // Defina a posição da câmera em relação ao jogador

    void LateUpdate()
    {
        if (jogador != null)
        {
            // A câmera segue apenas no eixo X, mantendo a posição Y original
            transform.position = new Vector3(jogador.position.x + offset.x, transform.position.y, offset.z);
        }
    }
}
