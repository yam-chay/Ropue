using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text meterText , scoreText , explainText , costText;
    [SerializeField] private GameObject losePanel , StorePanel, upgradePanel;
    [SerializeField] PhysicsMaterial2D physicsMaterial;
    private PlayerRope player;
    private Upgrade upgrade;
    [SerializeField] Image showUpgradeImage;
    private int cost;
    private UpgradeState upgradeState;
    void Start()
    {
        player = FindAnyObjectByType<PlayerRope>();
        StartCoroutine(nameof(ShowLosePanel));
        
    }
    IEnumerator ShowLosePanel()
    {
        yield return new WaitUntil(() => player.IsStopMoving());
        scoreText.text = ($"You Reach {Mathf.Round(player.transform.position.x)}M");
        losePanel.SetActive(true);

    }
    public void UpgradeButtonInfo(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        upgradePanel.gameObject.SetActive(true);
        cost = upgrade.cost;
        showUpgradeImage.sprite = upgrade.sprite;
        explainText.text = upgrade.Explain;
        costText.text = ($"Cost:{upgrade.cost}");
        upgradeState = upgrade.state;
    }
    public void UpgradeButton()
    {
        if(player.Coins >= cost)
        {
            switch (upgradeState)
            { 
                case UpgradeState.Bounciness:
                    physicsMaterial.bounciness += 0.1f;
                    break;
                         case UpgradeState.Volcano:
                    // add volcano force
                    break;
                case UpgradeState.Trampoline:
                    // add trampoline force
                    break;
                case UpgradeState.Friction:
                    physicsMaterial.friction -= 0.1f;
                    break;
                default:
                    Debug.Log("Idk");
                    break;
            }
            player.Coins -= cost;
            upgrade.cost += 400;
            costText.text = ($"Cost:{upgrade.cost}");

        }
        else
        {
           StartCoroutine(nameof(ColorCostText));
        }
    }
    IEnumerator ColorCostText()
    {
        costText.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        costText.color = Color.white;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        losePanel.SetActive(false);
    }
    public void ShowStorePanel()
    {
        StorePanel.SetActive(true);
        losePanel.SetActive(false);
    }
    public void BackButton()
    {
        StorePanel.SetActive(false);
        losePanel.SetActive(true);
        upgradePanel.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        meterText.text = ($"{Mathf.Round(Mathf.Clamp(player.transform.position.x, 0, 99999))}M");
        switch (player.transform.position.x)
        {
            case < 100:
                meterText.color = Color.white;
                break;
            case < 200:
                meterText.color = new Color(1, 1, 0, 1);
                break;
            case > 300:
                meterText.color = new Color(1, 0, 0, 1);
                break;
        }
    }
}
