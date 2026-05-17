using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private GameObject character;
    private SkeletonAnimation skelAnim;
    [SerializeField] private float moveDistance = 100f;
    private float originalX;

    [SerializeField] private GameObject[] buttons = new GameObject[6];
    private Image[] buttonImages = new Image[2];

    [SerializeField] private GameObject logo;
    private Image logoImage;

    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        skelAnim = character.GetComponent<SkeletonAnimation>();
        buttonImages[0] = buttons[0].GetComponent<Image>();
        buttonImages[1] = buttons[1].GetComponent<Image>();
        logoImage = logo.GetComponent<Image>();
        character.SetActive(false);
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
        logo.SetActive(false);
        Invoke(nameof(ShowCharacter), 1f);
        Invoke(nameof(ShowLogo), 2f);
        Invoke(nameof(ShowButtons), 3f);
        originalX = character.transform.position.x;
    }

    private void ShowCharacter()
    {
        skelAnim.Skeleton.SetColor(new Color(1, 1, 1, 0));
        character.SetActive(true);
        StartCoroutine(FadeInCharacter());
    }

    private void ShowLogo()
    {
        logoImage.color = new Color(1, 1, 1, 0);
        logo.SetActive(true);
        StartCoroutine(FadeInImage(logoImage));
    }

    private void ShowButtons()
    {
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }
        StartCoroutine(FadeInButtons());
    }

    private IEnumerator FadeInCharacter()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            float pos = Mathf.Lerp(-moveDistance, 0, time / fadeDuration);
            skelAnim.Skeleton.SetColor(new Color(1, 1, 1, alpha));
            character.transform.position = new Vector3(pos + originalX, character.transform.position.y, character.transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeInImage(Image img)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            img.color = new Color(1, 1, 1, alpha);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeInButtons()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            foreach (var img in buttonImages)
            {
                img.color = new Color(1, 1, 1, alpha);
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
}



