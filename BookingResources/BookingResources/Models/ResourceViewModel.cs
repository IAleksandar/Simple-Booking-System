namespace BookingResources.Models
{
    public class ResourceViewModel
    {
        public string Message { get; set; }
        public List<Domain.Resource> Resources { get; set; }

        public ResourceViewModel(string message, List<Domain.Resource> resources)
        {
            Message = message;
            Resources = resources;
        }
    }
}
