namespace Examiner.Domain
{
	public class ValueTaskVisitor : ITaskVisitor
	{
		public decimal Value { get; private set; }

		public ValueTaskVisitor()
		{
			Value = 0;
		}
	
		public void Visit(TaskElement element)
		{
			Value += element.Value;
		}

		public void Visit(TaskComposite composite)
		{
			foreach (ITaskComponent component in composite)
			{
				component.Accept(this);
			}
		}
	}
}
