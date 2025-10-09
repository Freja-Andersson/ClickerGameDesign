using UnityEngine;

public class ParrotClicker : MonoBehaviour
{

    bool canClick;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Parrot"))
        {
            Debug.Log("canClick becomes true");
            canClick = true;
        }
    }

    void OnMouseDown()
    {
        if (canClick)
        {
            Debug.Log("Parrot is clicked");
        }
    }
}
