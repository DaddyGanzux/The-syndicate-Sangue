using UnityEngine;
    public enum CharacterMovement
    {
        None = 0,
        Character1 = 1,
        Character2 = 2,
        Character3 = 3,
        Enemy1 = 4,
        Enemy2 = 5,
        Enemy3 = 6,
        Enemy4 = 7,
        Enemy5 = 8,
        Enemy6 = 9,
        Enemy7 = 10,
        Enemy8 = 11,
        Enemy9 = 12,
        Enemy10 = 13,
        Enemy11 = 14,
        Enemy12 = 15,
        Enemy13 = 16,
        Enemy14 = 17,
        Enemy15 = 18
    }

public class movimiento : MonoBehaviour
{
    public int team;
    public int currentX;
    public int currentY;
    public CharacterMovement type;

    private Vector3 desiredPosition; // Donde esta ahora
    private Vector3 desiredScale; // Donde debera estar


}
