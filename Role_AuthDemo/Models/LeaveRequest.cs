using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Role_AuthDemo.Models
{
    public class LeaveRequest
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }       // Link to Employee
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string? DocumentPath { get; set; }  // Uploaded file
        public string Status { get; set; } = "Pending"; // Pending / Approved / Rejected

        public int TotalDays
        {
            get
            {
                return (EndDate - StartDate).Days + 1;
            }
        }


    }
}
