using System;

namespace Examiner.Domain
{
	public class TaskElement : ITaskComponent
	{
		public string Name { get; }

		public string Description { get; }

		public decimal Value { get; }

		public TaskElement(string name, decimal value)
			: this(name, string.Empty, value)
		{
		}

		public TaskElement(string name, string description, decimal value)
		{
			TaskElement.ValidateInput(name, description, value);

			Name = name;
			Description = description;
			Value = value;
		}

		public void Accept(ITaskVisitor visitor)
		{
			visitor.Visit(this);
		}

		private static void ValidateInput(string name, string description, decimal value)
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

			if (value <= 0)
			{
				throw new ArgumentException($"{nameof(value)} must be greater than zero.");
			}
		}
	}
}
