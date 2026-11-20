using System.Collections;
using UnityEngine;

namespace Code
{
    public class AttackSpell : SpellBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float delay;
        [SerializeField] private Attack attack;

        protected override void OnCast(params Tile[] tiles)
        {
            var position = Vector3.zero;
            foreach (var tile in tiles)
            {
                position += tile.transform.position;
            }
            position /= tiles.Length;
            transform.position = position;
            
            if (delay > 0)
            {
                if (animator) animator.SetTrigger("Cast");
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
            attack.Perform();
        }
    }
}