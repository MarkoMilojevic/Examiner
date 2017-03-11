namespace Examiner.Domain
{
	public interface ITaskComponent
	{
		string Name { get; }

		string Description { get; }

		void Accept(ITaskVisitor visitor);
	}
}
