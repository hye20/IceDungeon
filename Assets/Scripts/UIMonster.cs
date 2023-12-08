using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonster : MonoBehaviour
{
    private float _current;
    private float _target;
    private Image _healthBarImg;
    [SerializeField] private TMP_Text UImonsterName;
    // Start is called before the first frame update
    void Start()
    {
        _healthBarImg = GetComponent<Image>();
        _current = 100;
        _target = 100;
    }

    // Update is called once per frame
    void Update()
    {
        _current = Mathf.Lerp(_current, _target, 0.025f);
        _healthBarImg.fillAmount = _current;
    }
    public void SetName(string name)
    {
        UImonsterName.SetText(name);
    }

    public void UpdateTatgetValue(float currentHP,float maxHP)
    {
        _target = currentHP/maxHP;
    }
}
