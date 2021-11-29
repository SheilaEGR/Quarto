
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Text poolText;
    public Text boardText;
    public ResultGUI resultGUI;

    public void SetPlayerSelect(int player)
    {
        poolText.gameObject.SetActive(true);
        boardText.gameObject.SetActive(false);

        poolText.text = "Player " + player.ToString() + " choose";
    }

    public void SetPlayerPlace(int player)
    {
        poolText.gameObject.SetActive(false);
        boardText.gameObject.SetActive(true);

        boardText.text = "Player " + player.ToString() + " place";
    }

    public void ShowResult(int player)
    {
        resultGUI.gameObject.SetActive(true);
        resultGUI.ShowResult(player);
    }

    public void HideResult()
    {
        resultGUI.gameObject.SetActive(false);
    }
}
