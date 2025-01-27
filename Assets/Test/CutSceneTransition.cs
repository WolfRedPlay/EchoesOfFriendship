using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTransition : MonoBehaviour
{
    [SerializeField] Camera _cutSceneCameraObject;
    [SerializeField] CinemachineBrain _brain;
    [SerializeField] GameObject _virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TranOn());
    }

    IEnumerator TranOn()
    {
        yield return new WaitForSeconds(2f);

        float elapsedTime = 0f;
        _virtualCamera.SetActive(true);
        float fadeDuration = 4f;
        Rect viewportRect = _cutSceneCameraObject.rect;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            viewportRect.width = Mathf.Lerp(viewportRect.width, 1f, elapsedTime/ fadeDuration);
            _cutSceneCameraObject.rect = viewportRect;
            yield return null;
        }
        //StartCoroutine(TranOff());
    }

    IEnumerator TranOff()
    {
        yield return new WaitForSeconds(5f);
        float elapsedTime = 0f;
        _virtualCamera.SetActive(false);
        float fadeDuration = 4f;
        Rect viewportRect = _cutSceneCameraObject.rect;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            viewportRect.width = Mathf.Lerp(viewportRect.width, .5f, elapsedTime / fadeDuration);
            _cutSceneCameraObject.rect = viewportRect;
            yield return null;
        }
        StartCoroutine(TranOff());
    }
}
