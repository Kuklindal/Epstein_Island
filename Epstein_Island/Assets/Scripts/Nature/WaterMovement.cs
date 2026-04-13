using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public float speed = 0.5f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        Debug.Log("WORKING");
        float offset = Time.time * speed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
    }
}