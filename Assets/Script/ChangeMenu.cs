using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class ChangeMenu : MonoBehaviour
{
    public void LoadAssembly()
    {
        SceneManager.LoadScene("Assembly_menu");
    }

    public void LoadAssemblyGuided()
    {
        SceneManager.LoadScene("Assembly_guided");
    }

    public void LoadAssemblyPerformance()
    {
        SceneManager.LoadScene("Assembly_performance_1");
    }


    public void LoadDisassembly()
    {
        SceneManager.LoadScene("Disassembly_menu");
    }

    public void LoadDisassemblyGuided()
    {
        SceneManager.LoadScene("Disassembly_guided");
    }

    public void LoadDisassemblyPerformance()
    {
        SceneManager.LoadScene("Disassembly_performance");
    }


    public void LoadInteractionMenu()
    {
        SceneManager.LoadScene("Interaction_menu");
    }

    public void LoadInteractionInfo()
    {
        SceneManager.LoadScene("Interaction_information");
    }

    public void LoadInteractionManual()
    {
        SceneManager.LoadScene("Interaction_manual_interaction");
    }

    public void LoadInteractionAnimation()
    {
        SceneManager.LoadScene("Interaction_animation");
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_menu");
    }
}