namespace MouseWorkflowAutomation.Models
{
    public class ImageClickAction : WorkflowAction
    {
        public string? ImagePath { get; set; }

        public ImageClickAction()
        {
            ActionType = "Image Click";
        }
    }
}