using System.Collections;
using UnityEngine;

namespace Code
{
    public class AttackSpell : SpellBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float delay;
        [SerializeField] private Attack attack;

        public override void Cast(Tile tile)
        {
            transform.position = tile.transform.position;
            if (animator && delay > 0) animator.SetTrigger("Cast");
            StartCoroutine(DelayedSpellCast());
        }

        private IEnumerator DelayedSpellCast()
        {
            yield return new WaitForSeconds(delay);
            if (animator)
            {
                animator.SetTrigger("Attack");
            }
            yield return new WaitForSeconds(0.6f);
            attack.Perform();
        } 
    }
}