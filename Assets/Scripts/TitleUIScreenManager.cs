using UnityEngine;

public class TitleUIScreenManager : MonoBehaviour
{
    public GameObject androidUI;
    public GameObject desktopUI;

    void Start()
    {
#if UNITY_ANDROID
        androidUI.SetActive(true);
            desktopUI.SetActive(false);
        Debug.Log("Running on Android");

#elif UNITY_STANDALONE || UNITY_EDITOR
        androidUI.SetActive(false);
            desktopUI.SetActive(true);
        Debug.Log(" Running on Mac");
#endif
    }
}
