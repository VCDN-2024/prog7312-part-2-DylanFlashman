using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp.MVVM.Model
{
    public class IssueForm
    {
        public string Address { get; set; }
        public string Suburb {  get; set; }
        public string PostalCode { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string? Attachment { get; set; }
    }
}
