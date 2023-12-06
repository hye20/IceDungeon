using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HP = 100;
    public int atk;
    public int SP;
    public string[] skills;
    public string[] actions;
    public bool is_dead;

    private Animator animator;
    private bool playAnim;
    // Start is called before the first frame update
    void Start()
    {
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
            gameObject.SetActive(false);//play anim
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
    }
    public void MonsterAnim(string animName)//argument = akt, def, counter
    {
        animator.SetBool(animName, true);
        playAnim = true;
    }
    public void AttackEffect()
    {
        
    }
}
