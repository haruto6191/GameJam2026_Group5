using UnityEngine;

public class S_MovePannel : MonoBehaviour
{
    [SerializeField] private GameObject stageSelectPannel;
    [SerializeField] private GameObject normalPannel;

    private Animator selectAnim;
    private Animator normalAnim;

    private void Start()
    {
        selectAnim = stageSelectPannel.GetComponent<Animator>();
        normalAnim = normalPannel.GetComponent<Animator>();

        stageSelectPannel.SetActive(false);
    }

    public void OpenSelectPannel()
    {
        normalAnim.SetTrigger("Close");
        Invoke("ActivSelectPannel", 1f);
    }

    private void ActivSelectPannel()
    {
        stageSelectPannel.SetActive(true);
        normalAnim.gameObject.SetActive(false);
    }

    public void OpenNormalPannel()
    {
        selectAnim.SetTrigger("Close");
        Invoke("ActivNormalPannel", 1f);
    }

    private void ActivNormalPannel()
    {
        normalPannel.SetActive(true);
        selectAnim.gameObject.SetActive(false);
    }
}
