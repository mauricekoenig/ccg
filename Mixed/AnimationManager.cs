



using UnityEngine;
using DG.Tweening;

public class AnimationManager : MonoBehaviour, IAnimationManager {

    private static bool isAnimating;
    public static bool IsAnimating => isAnimating;

    [SerializeField] private DamageIndicator damageIndicator;

    public void Attack (ICardView attacker, ICardView defender) {

        if (isAnimating) return;
        isAnimating = true;
        Vector3 origin = attacker.Transform.position;
        attacker.Canvas.sortingOrder = 100;

        var attacker_creature = attacker.Data as CreatureRuntimeCardData;
        var defender_creature = defender.Data as CreatureRuntimeCardData;

        var sequence = DOTween.Sequence();

        sequence.
            Append(attacker.Transform.DOMove(defender.Transform.position, .3f)).
            AppendCallback(() => { damageIndicator.Show(attacker_creature.Attack, defender.Transform.position); }).
            Append(attacker.Transform.DOMove(origin, .3f)).
            AppendCallback(() => { 

                damageIndicator.Hide(); 
                attacker.Canvas.sortingOrder = 0; 
                isAnimating = false;
                attacker_creature.PerformAttack(defender_creature);
            });
    }
}