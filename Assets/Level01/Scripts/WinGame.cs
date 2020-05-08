using UnityEngine;

public class WinGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("You Win");
    }
}
