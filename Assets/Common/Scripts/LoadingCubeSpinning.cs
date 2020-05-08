using UnityEngine;

public class LoadingCubeSpinning : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Rotate(speed / 4, speed, speed / 2);
    }
}
