using UnityEngine;

public class Monster : MonoBehaviour
{
    /// <summary>
    /// monster Index
    /// </summary>
    public int index;
    
    public float HP = 100;
    public float maxHP = 100;
    public float atk;
    public float SP;
    public float speed;
    public int priority;

    public string[] actions;//atk, defense, counter
    public string[] skills;

    public bool is_dead;
    public bool able_target;

    private SpriteRenderer renderer;
    private Animator animator;
    private Collider2D col;
    [SerializeField] private GameObject targetArrow;
    private bool playAnim;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        targetArrow = Instantiate(targetArrow);
        targetArrow.transform.position = transform.position + new Vector3(0, col.bounds.extents.y * 1.1f, 0);
        targetArrow.SetActive(false);
        animator = GetComponent<Animator>();
        HP = 100f;
        atk = 5f;
        SP = 10f;
        is_dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            is_dead = true;
        }
        else is_dead = false;
        if (playAnim)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f
                && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.SetBool("Attack", false);
                playAnim = false;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f
                && animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
            {
                animator.SetBool("Skill", false);
                playAnim = false;
            }
        }
        if (is_dead)
        {
            float alpha = renderer.material.color.a;
            alpha -= 0.01f;
            renderer.material.color = new Color(1, 1, 1, alpha);
            if (renderer.material.color.a <= 0f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    public void MonsterAnim(string animName)//argument = akt, def, counter
    {
        animator.SetBool(animName, true);
        playAnim = true;
    }
    public void AttackEffect()
    {

    }
    private void OnMouseEnter()
    {
        if (able_target) targetArrow.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (able_target) targetArrow.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (able_target)
        {
            targetArrow.SetActive(false);
            BattleManager.instance.PlayerAtk(index);
        }
    }
}
