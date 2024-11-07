using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace MTecl.GraphQlClient.Exceptions
{
    public class ServerErrorResponseException : Exception
    {
        public string RawResponse { get; }
                
        public List<ErrorModel> Errors { get; }

        public ServerErrorResponseException(string rawResponse, IList<ErrorModel> errors):base(FormatErrorsMessage(errors)  ?? rawResponse) 
        {
            RawResponse = rawResponse;
            Errors = new List<ErrorModel>(errors ?? new ErrorModel[0]);
        }

        private static string FormatErrorsMessage(IList<ErrorModel> errors)
        {
            if (errors == null)
                return null;

            var grouped = errors.GroupBy(e => e.Message);

            var sb = new StringBuilder();


            var groupIndex = 0;
            var totalReported = 0;
            foreach (var group in grouped)
            {
                if (groupIndex > 0)
                    sb.AppendLine();

                var groupSize = group.Count();
                if (groupIndex > 2)
                {
                    sb.Append($"... And {errors.Count - totalReported} more error(s) - See {nameof(ServerErrorResponseException)}.{nameof(Errors)}");
                    break;
                }

                var firstError = group.First();
                sb.Append(firstError);

                if (groupSize > 1)
                {
                    sb.Append($" (and {groupSize - 1} more occurences)");
                }

                groupIndex++;
                totalReported += groupSize;
            }

            return sb.ToString();
        }

        #region Model
        

        public class ErrorModel
        {
            public string Message { get; set; }
            public List<object> Path { get; set; }
            public List<ErrorLocation> Locations { get; set; }

            public override string ToString()
            {
                var parts = new string[] {
                   Message ?? "ERROR",
                   Locations == null || Locations.Count == 0 ? string.Empty : $"[{string.Join(", ", Locations)}]",
                   Path == null || Path.Count == 0 ? string.Empty : string.Join("/", Path)
                };

                return string.Join(" ", parts);
            }
        }

        public class ErrorLocation
        {
            public int Line { get; set; }
            public int Column { get; set; }

            public override string ToString()
            {
                return $"ln: {Line}, col: {Column}";
            }
        }
        #endregion

    }
}
