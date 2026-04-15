using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class PieceMoving : MonoBehaviour
{
    private bool isDragging;
    private Vector3 offset;

    void Update()
    {
        if (Mouse.current == null) return;

        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        mouseScreen.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Collider2D hit = Physics2D.OverlapPoint(mouseWorld);

            if (hit.GetComponent<Collider2D>() != null && hit.transform == transform)
            {
                isDragging = true;
                offset = transform.position - mouseWorld;
            }
        }

        if (Mouse.current.leftButton.isPressed && isDragging)
        {
            transform.position = mouseWorld + offset;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
            DropPiece();
        }
    }
    void DropPiece()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);
        Vector3 snapPosition = new Vector3(x, y, transform.position.z);
        transform.position = snapPosition;
    }
}