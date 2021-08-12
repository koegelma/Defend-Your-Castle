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




    private void Start()
    {
        Money = startMoney;
        Life = startLife;
    }

    private void Update()
    {
        moneyText.text = (Money + "G");

        HndLife();
    }


    private void HndLife()
    {
        if (Life <= 0)
        {
            if (GameManager.instance.isGameOver) { return; }
            GameManager.instance.GameOver();
            Life = 0;
        }

        lifeText.text = (Life + " Health");
    }
}
