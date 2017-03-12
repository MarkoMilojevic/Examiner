using System;

namespace Examiner.Domain
{
	public class Penalty
	{
		public string Name { get; }

		public string Description { get; }

		public string Category { get; }

		public Severity Severity { get; }

		public Penalty(string name, string category, Severity severity)
			: this(name, string.Empty, category, severity)
		{
		}

		public Penalty(string name, string description, string category, Severity severity)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Category = category ?? throw new ArgumentNullException(nameof(category));
			Severity = severity;
		}
	}
}
