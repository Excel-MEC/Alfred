using Alfred.Client.Dtos.Accounts;
using Alfred.Client.Models;
using System.Collections.Generic;

namespace Alfred.Client.Dtos.Results
{
    public class ResultsFromRepo
    {
       public bool IsTeam { get; set; }
       public List<ResultsForViewDto> Results { get; set; }
    }
}