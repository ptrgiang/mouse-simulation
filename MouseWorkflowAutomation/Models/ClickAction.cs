namespace MouseWorkflowAutomation.Models
{
    public class ClickAction : WorkflowAction
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ClickAction()
        {
            ActionType = "Click";
        }
    }
}
