namespace Presentation.ViewModels
{
    public class AddReservationRequest
    {
        public string Token { get; set; }      
        public string StartTime { get; set; }  
        public int CourtId { get; set; }
    }
}
