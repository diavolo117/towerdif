using UnityEngine;
using UnityEngine.SceneManagement;

public class backbutton : MonoBehaviour
{
    public void backto()
    {
        SceneManager.LoadScene("lvlpickup");
    }
}
