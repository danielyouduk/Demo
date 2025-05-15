namespace Services.Core.ServiceBus;

public static class ServiceBusConstants
{
    public static class Topics
    {
        public static class Driver
        {
            public const string Created = "driver.events.created";
            
            public static class Subscriptions
            {
                public const string SearchService = "search-service.driver-indexing";
            }
        }
        
        public static class Checklist
        {
            public const string Created = "checklist.events.created";
            public const string Submitted = "checklist.events.submitted";
            
            public static class Subscriptions
            {
                public const string FleetManagementCreated = "fleet-management.checklist-stats.created";
                public const string FleetManagementSubmitted = "fleet-management.checklist-stats.submitted";
                public const string DocumentProcessorSubmitted = "document-processor.checklist-stats.submitted";
            }
        }
        
        public static class DeadLetter
        {
            public const string DriverCreated = "deadletter.driver.events.created";
            public const string ChecklistCreated = "deadletter.checklist.events.created";
            public const string ChecklistSubmitted = "deadletter.checklist.events.submitted";

            public static class Subscriptions
            {
                public const string DriverSearchService = "deadletter.search-service.driver-indexing";
                public const string ChecklistFleetManagementCreated = "deadletter.fleet-management.checklist-stats.created";
                public const string ChecklistFleetManagementSubmitted = "deadletter.fleet-management.checklist-stats.submitted";
            }
        }
    }
}