using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Skills/Double Sword Swing")]
public class DoubleSwordSwing : SwordSwing
{
    [Header("Skill Info")]
    [SerializeField] private Vector2 extentsFromTarget;
    [SerializeField] private MeleeWeapon sword;
    [SerializeField] private float creationWaitTime;
    [SerializeField] private float baseDamage;
    [SerializeField] private float strWeight;

    public override void TriggerSkill(Attacker user, GameObject target)
    {
        user.StartCoroutine(Attack(user, target, -1f));
        user.StartCoroutine(Attack(user, target, 1f));
    }

    private IEnumerator Attack(Attacker user, GameObject target, float dir)
    {
        yield return new WaitForSeconds(creationWaitTime);

        var parent = new GameObject();
        parent.transform.position = target.transform.position + new Vector3(dir * extentsFromTarget.x, extentsFromTarget.y, 0f);

        Vector3 parentScale = parent.transform.localScale;
        parentScale.x *= dir;
        parent.transform.localScale = parentScale;

        MeleeWeapon sword = Instantiate(this.sword, parent.transform);

        var fade = sword as IFade;

        float t = fade?.FadeIn() ?? -1;
        if (t > 0)
            yield return new WaitForSeconds(t);

        if (sword is IAttackTime)
            (sword as IAttackTime).AttackTime = this.swingTime;

        sword.Attack(baseDamage + strWeight * GetSTR(user));
        yield return this.Swing(sword);

        t = fade?.FadeOut() ?? -1;
        if (t > 0)
            yield return new WaitForSeconds(t);

        Debug.Log("파괴");
        Destroy(parent);
    }
}
