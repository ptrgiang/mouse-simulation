namespace MouseWorkflowAutomation.Models
{
    public class DelayAction : WorkflowAction
    {
        public int Milliseconds { get; set; }

        public DelayAction()
        {
            ActionType = "Delay";
        }
    }
}
