using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceStarr.GlobalErrorHandling
{
    public class ValidationResponse:ErrorResponse
    {
        public ValidationResponse():base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}