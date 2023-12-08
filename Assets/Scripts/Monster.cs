using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HP = 100;
    public int maxHP = 100;
    public int atk;
    public int SP;
    public int index;
    public string[] skills;
    public string[] actions;
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
        HP = 100;
        atk = 5;
        SP = 10;
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
            Debug.Log(renderer.material.color);
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
