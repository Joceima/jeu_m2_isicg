using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] Button StartButton;
    [SerializeField] Button EndButton;

    private void Start()
    {
        StartButton.onClick.AddListener(() => ChangeScene("SampleScene"));
        EndButton.onClick.AddListener(() => Application.Quit());
    }

    void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
