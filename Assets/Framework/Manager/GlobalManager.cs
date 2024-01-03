using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PopupTest.Get.Open();
        }
    }
#endif
}