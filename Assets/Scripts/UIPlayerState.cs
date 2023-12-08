using UnityEngine;
using UnityEngine.UI;

public class UIPlayerState : MonoBehaviour
{
    public Image image;
    public float current;
    public float target;
    // Start is called before the first frame update
    void Awake()
    {
        target = GameManager.instance.player.HP / GameManager.instance.player.MaxHP;
        current = target;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = GameManager.instance.player.HP / GameManager.instance.player.MaxHP;
        current = Mathf.Lerp(current, target,0.05f);
        image.fillAmount = current;
    }
}
