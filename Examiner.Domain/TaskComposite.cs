using System;
using System.Collections;
using System.Collections.Generic;

namespace Examiner.Domain
{
	public class TaskComposite : ITaskComponent, IEnumerable<ITaskComponent>
	{
		private readonly List<ITaskComponent> _components;

		public string Name { get; }

		public string Description { get; }

		public TaskComposite(string name)
			: this(name, string.Empty, new List<ITaskComponent>())
		{
		}

		public TaskComposite(string name, string description)
			: this(name, description, new List<ITaskComponent>())
		{
		}

		public TaskComposite(string name, List<ITaskComponent> components)
			: this(name, string.Empty, components)
		{
		}

		public TaskComposite(string name, string description, List<ITaskComponent> components)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Description = description ?? throw new ArgumentNullException(nameof(description));

			_components = components ?? throw new ArgumentNullException(nameof(components));
		}

		public void Accept(ITaskVisitor visitor)
		{
			visitor.Visit(this);
		}

		public void Add(ITaskComponent component)
		{
			if (component == null)
			{
				throw new ArgumentNullException(nameof(component));
			}

			_components.Add(component);
		}

		public void Remove(ITaskComponent component)
		{
			_components.Remove(component);
		}

		public bool Contains(ITaskComponent component)
		{
			return _components.Contains(component);
		}

		public IEnumerator<ITaskComponent> GetEnumerator()
		{
			return _components.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _components.GetEnumerator();
		}
	}
}
