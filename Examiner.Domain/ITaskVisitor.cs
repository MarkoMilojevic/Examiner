namespace Examiner.Domain
{
	public interface ITaskVisitor
	{
		void Visit(TaskElement element);

		void Visit(TaskComposite composite);
	}
}
