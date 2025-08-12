namespace MouseWorkflowAutomation.Models
{
    public class RightClickAction : WorkflowAction
    {
        public int X { get; set; }
        public int Y { get; set; }

        public RightClickAction()
        {
            ActionType = "Right Click";
        }
    }
}
