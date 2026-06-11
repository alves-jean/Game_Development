using UnityEngine;

public class WeightTrigger : MonoBehaviour
{
    public FallingWeight weight;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            weight.Drop();
        }
    }
}