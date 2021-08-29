using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;
    public Text moneyText;

    public static int Life;
    public int startLife = 100;
    public Text lifeText;

    public static int Rounds;

    private void Start()
    {
        Money = startMoney;
        Life = startLife;
        Rounds = 0;
    }

    private void Update()
    {
        moneyText.text = (Money + "G");

        HndLife();
    }

    private void HndLife()
    {
        if (GameManager.instance.isGameOver) return;
        if (Life <= 0)
        {
            GameManager.instance.GameOver();
            Life = 0;
        }
        lifeText.text = (Life + " HEALTH");
    }
}
