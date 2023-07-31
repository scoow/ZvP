using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    const float _background_height = 1080f;
    const float _background_width = 1080f;
    private void Start()
    {
        Debug.Log(Screen.height);
        transform.localScale = new Vector2(transform.localScale.x, 2 * Screen.height / _background_height);
    }
}