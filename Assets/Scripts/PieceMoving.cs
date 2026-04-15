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
            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);

            if (hit.collider != null && hit.transform == transform)
            {
                isDragging = true;
                offset = transform.position - (Vector3)mouseWorld;
            }
        }

        if (Mouse.current.leftButton.isPressed && isDragging)
        {
            transform.position = (Vector3)mouseWorld + offset;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }
    }
}