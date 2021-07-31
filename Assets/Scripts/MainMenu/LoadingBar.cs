using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Image _loadingBarImage;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadRoutine());
    }

    IEnumerator LoadRoutine()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2);
        while (!asyncOperation.isDone)
        {
            _loadingBarImage.fillAmount = asyncOperation.progress;
            yield return new WaitForEndOfFrame();
        }
    }

}
