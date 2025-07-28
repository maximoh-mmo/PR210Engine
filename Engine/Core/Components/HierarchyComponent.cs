using Engine.Core.Interfaces;
using System.Collections.ObjectModel;

namespace Engine.Core.Components
{
    public class HierarchyComponent() : IComponent
    {
        private readonly List<GameObject> _children = new List<GameObject>();
        public GameObject? Parent { get; private set; }

        public ReadOnlyCollection<GameObject> Children => _children.AsReadOnly();

        public bool HasChildren => _children.Count > 0;
        public bool HasParent => Parent != null;
        public bool IsRoot => Parent == null;

        public static void SetParent(GameObject parent, GameObject child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            var hierarchyComponent = child.GetComponent<HierarchyComponent>();
            if (hierarchyComponent == null)
            {
                throw new InvalidOperationException("Child GameObject must have a HierarchyComponent to set parent.");
            }
            hierarchyComponent.Parent = parent;
            if (parent.GetComponent<HierarchyComponent>() is { } parentHierarchyComponent)
            {
                parentHierarchyComponent._children.Add(child);
            }
        }
        public static void RemoveParent(GameObject child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            var hierarchyComponent = child.GetComponent<HierarchyComponent>();
            if (hierarchyComponent == null)
            {
                throw new InvalidOperationException("Child GameObject must have a HierarchyComponent to set parent.");
            }
            hierarchyComponent.Parent = null;
        }
    }
}
