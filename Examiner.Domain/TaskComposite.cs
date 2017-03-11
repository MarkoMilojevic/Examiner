using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
			TaskComposite.ValidateInput(name, description, components);

			Name = name;
			Description = description;

			_components = components;
		}

		private static void ValidateInput(string name, string description, List<ITaskComponent> components)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (name == string.Empty)
			{
				throw new ArgumentException($"{nameof(name)} cannot be empty string.");
			}

			if (name.Contains(" "))
			{
				throw new ArgumentException($"{nameof(name)} cannot contain whitespace.");
			}

			if (description == null)
			{
				throw new ArgumentNullException(nameof(description));
			}

			if (components == null)
			{
				throw new ArgumentNullException(nameof(components));
			}

			if (components.Any(c => c == null))
			{
				throw new ArgumentException($"{nameof(TaskComposite)} cannot contain nulls.");
			}
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
