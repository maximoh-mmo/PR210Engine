using Engine.Core.Components;
using OpenTK.Mathematics;

namespace Engine.Core.Systems
{
    public class TransformSystem
    {
        public void Update (List<GameObject> gameObjects)
        { 
            var rootObjects = gameObjects.Where(go => go.GetComponent<HierarchyComponent>()?.IsRoot == true).ToList();
            foreach (var root in rootObjects)
            {
                var hierarchyComponent = root.GetComponent<HierarchyComponent>();
                if (hierarchyComponent == null) continue;
                var rootWorldTranformComponent = root.GetComponent<TransformComponent>();
                if (rootWorldTranformComponent == null) continue;
                UpdateTransformHierarchy(root, Matrix4.Identity);
            }

            // After processing root objects, update remaining game objects
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.GetComponent<HierarchyComponent>() == null &&
                    gameObject.GetComponent<TransformComponent>() != null)
                {
                    UpdateTransformHierarchy(gameObject, Matrix4.Identity);
                }
            }
        }

        private void UpdateTransformHierarchy(GameObject gameObject, Matrix4 parentWorldMatrix)
        {
            var transform = gameObject.GetComponent<TransformComponent>();
            if (transform != null)
            {
                transform.WorldMatrix = CreateLocalTransformMatrix(transform) * parentWorldMatrix;
            }

            var hierarchyComponent = gameObject.GetComponent<HierarchyComponent>();
            
            if (hierarchyComponent == null || !hierarchyComponent.HasChildren) return;
            
            foreach (var child in hierarchyComponent.Children)
            {
                UpdateTransformHierarchy(child, transform?.WorldMatrix ?? parentWorldMatrix);
            }
        }

        private static Matrix4 CreateLocalTransformMatrix(TransformComponent transform)
        {
            return transform.LocalMatrix;
        }
    }
}