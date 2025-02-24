using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text meterText , scoreText , explainText , costText;
    [SerializeField] private GameObject losePanel , StorePanel, upgradePanel;
    private PlayerRope player;
    bool onlyone = true ;
    private Upgrade upgrade;
    [SerializeField] Image showUpgradeImage;
    private int cost;
    void Start()
    {
        player = FindAnyObjectByType<PlayerRope>();
        
    }
    public void UpgradeButton(Upgrade upgrade)
    {
        if(upgrade.enabled == false)
        {
            upgrade.gameObject.SetActive(true);
        }
        cost = upgrade.cost;
        showUpgradeImage.sprite = upgrade.image;
        explainText.text = upgrade.Explain;
        costText.text = ($"Cost:{upgrade.cost}");
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
