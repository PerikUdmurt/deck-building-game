using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public class TargetFinder
    {
        private readonly Collider2D _collider;

        public TargetFinder(Collider2D collider) 
        {
            _collider = collider;
        }

        public bool FindNearestCardTarget(LayerMask layerMask, out ICardTarget cardTarget)
        {
            cardTarget = null;
            List<Collider2D> colliders = FindAllCollider(layerMask);
            if (colliders.Count == 0) return false;

            var targets = from c in colliders
                          where c.gameObject.TryGetComponent<ICardTarget>(out ICardTarget a)
                          orderby (c.transform.position - _collider.transform.position).magnitude
                          select c;
            
            Collider2D target = targets.FirstOrDefault();
            if (target == null) return false;

            target.TryGetComponent<ICardTarget>(out var result);
            cardTarget = result;
            return true;
        }

        private List<Collider2D> FindAllCollider(LayerMask layerMask)
        {
            Vector2 leftBottomCorner = new(_collider.bounds.min.x, _collider.bounds.min.y);
            Vector2 rightTopCorner = new(_collider.bounds.max.x, _collider.bounds.max.y);
            Collider2D[] colliders = Physics2D.OverlapAreaAll(leftBottomCorner, rightTopCorner, layerMask);
            return colliders.ToList();
        }
    }
}