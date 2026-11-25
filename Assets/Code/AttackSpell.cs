using System.Collections;
using UnityEngine;

namespace Code
{
    public class AttackSpell : SpellBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float delay;
        [SerializeField] private Attack attack;
        [SerializeField] private bool animatorCastSpell = false;

        protected override void OnCast(params Tile[] tiles)
        {
            var position = Vector3.zero;
            foreach (var tile in tiles)
            {
                position += tile.transform.position;
            }
            position /= tiles.Length;
            position += (Vector3.up * 0.2f);
            transform.position = position;
            
            if (delay > 0)
            {
                StartCoroutine(DelayedSpellCast());
            }
            else
            {
                DoCast();
            }
        }

        private IEnumerator DelayedSpellCast()
        {
            yield return new WaitForSeconds(delay);
            DoCast();
        }

        private void DoCast()
        {
            if (animator) animator.SetTrigger("Attack");
            if (!animatorCastSpell) attack.Perform();
        }
    }
}