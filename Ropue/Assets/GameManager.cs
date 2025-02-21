using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text meterText , scoreText;
    [SerializeField] private GameObject losePanel , StorePanel; 
    private PlayerRope player;
    bool onlyone = true ;
    void Start()
    {
        player = FindAnyObjectByType<PlayerRope>();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        losePanel.SetActive(false);
    }
    public void ShowStorePanel()
    {
        onlyone = false;
        StorePanel.SetActive(true);
        losePanel.SetActive(false);
    }
    public void BackButton()
    {
        onlyone = true;
        StorePanel.SetActive(false);
        losePanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        meterText.text = ($"{Mathf.Round(Mathf.Clamp(player.transform.position.x, 0, 99999))}M");
            scoreText.text = ($"You Reach {Mathf.Round(player.transform.position.x)}M");
        if (onlyone == true) 
        { 
            losePanel.SetActive(player.IsStopMoving());
        }
    }
}
