using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class PieceMoving : MonoBehaviour
{
    private bool isDragging;
    private Vector3 offset;
    private Vector2Int originalPosition;

    void Update()
    {
        if (Mouse.current == null) return;

        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        mouseScreen.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Collider2D hit = Physics2D.OverlapPoint(mouseWorld);

            if (hit != null && hit.transform == transform)
            {
                isDragging = true;
                originalPosition = GetVector2IntPosition(transform.position);
                offset = transform.position - mouseWorld;
            }
        }
        if (isDragging)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                transform.position = mouseWorld + offset;
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                isDragging = false;
                DropPiece();
            }
        } 
    }
    void DropPiece()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

        Vector2Int newPosition = new(x,y);
        GameManager.Instance.MovePiece(originalPosition, newPosition, this);
    }
    public void ResetPosition() => transform.position = new Vector3(originalPosition.x, originalPosition.y, transform.position.z);
    public void ChangePosition(Vector2Int newPostion) => transform.position = new Vector3(newPostion.x, newPostion.y, transform.position.z);
    private Vector2Int GetVector2IntPosition(Vector3 position) => new Vector2Int(Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y));
}