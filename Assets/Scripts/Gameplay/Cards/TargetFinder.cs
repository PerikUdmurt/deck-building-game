using CardBuildingGame.Gameplay.Characters;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public class TargetFinder
    {
        public bool FindNearestCardTarget(Collider2D collider, TargetLayer targetLayer, out ICardTarget cardTarget)
        {
            cardTarget = null;
            List<Collider2D> colliders = FindAllColliders(collider);
            List<ICardTarget> allTargets = new List<ICardTarget>();
            if (colliders.Count == 0) return false;

            var targets = from c in colliders
                          where c.gameObject.TryGetComponent<ICardTarget>(out ICardTarget a)
                          orderby (c.transform.position - collider.transform.position).magnitude
                          select c;

            foreach (var coll in targets) 
            { 
                coll.TryGetComponent<ICardTarget>(out cardTarget);
                allTargets.Add(cardTarget);
            }
                
            ICardTarget totalTarget = allTargets.FirstOrDefault(c => c.TargetLayer == targetLayer);
            if (totalTarget == null) return false;

            cardTarget = totalTarget;
            return true;
        }

        public ICardTarget FindRandomTarget(IEnumerable<ICardTarget> cardTargets, TargetLayer targetLayer)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in cardTargets)
            {
                sb.AppendLine($"{c.TargetLayer}");
            }
            sb.AppendLine($"{targetLayer}");

            var availableTargets = from target in cardTargets
                                   where target.TargetLayer == targetLayer
                                   select target;

            return availableTargets.FirstOrDefault();
        }

        private List<Collider2D> FindAllColliders(Collider2D collider)
        {
            Vector2 leftBottomCorner = new(collider.bounds.min.x, collider.bounds.min.y);
            Vector2 rightTopCorner = new(collider.bounds.max.x, collider.bounds.max.y);
            Collider2D[] colliders = Physics2D.OverlapAreaAll(leftBottomCorner, rightTopCorner);
            return colliders.ToList();
        }
    }
}