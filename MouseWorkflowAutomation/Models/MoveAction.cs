namespace MouseWorkflowAutomation.Models
{
    public class MoveAction : WorkflowAction
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MoveAction()
        {
            ActionType = "Move";
        }
    }
}
