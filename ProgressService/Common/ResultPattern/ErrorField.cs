using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ErrorField
    {
        public string Field {  get; set; }  
        public string ErrorMessage { get; set; }

        public ErrorField(string field, string message)
        {
            Field = field;
            ErrorMessage = message;
        }
    }
}
