using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp.MVVM.Model
{
    public class ServiceRequest
    {
        public ServiceRequest(int id, string description, string status, int priority, DateTime dateSubmitted)
        {
            Id = id;
            Description = description;
            Status = status;
            Priority = priority;
            DateSubmitted = dateSubmitted;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Status {  get; set; }
        public int Priority { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
