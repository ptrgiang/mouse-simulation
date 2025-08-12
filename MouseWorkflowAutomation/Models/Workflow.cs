using System.Collections.Generic;
using MouseWorkflowAutomation.Models;

namespace MouseWorkflowAutomation
{
    public class Workflow
    {
        public List<WorkflowAction> Actions { get; set; }

        public Workflow()
        {
            Actions = new List<WorkflowAction>();
        }
    }
}
