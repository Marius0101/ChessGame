using UnityEngine;

public class BoardSpawning : MonoBehaviour
{
    public GameObject squareTemplate;
    public int numberOfSquares = 8;
    void Start()
    {
        for (int i = 0; i < numberOfSquares; i++)
        {
            for(int j = 0; j < numberOfSquares; j++)
            {
                Vector3 position = new Vector3(i, j, 0);
                
                GameObject square = Instantiate(squareTemplate, position, Quaternion.identity);
                Renderer renderer = square.GetComponent<Renderer>();
                if ((i + j) % 2 == 0)
                {
                    renderer.material.color = Color.white;
                }
                else
                {
                    renderer.material.color = Color.black;
                }
            }
        }
    }
}
