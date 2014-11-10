using System.Collections.Generic;
using System.Web.Http;

namespace HttpDemo.Controllers
{
    public class TitleController : ApiController
    {
	    public IEnumerable<string> Get()
	    {
		    return new List<string>
					{
						"Captain", 
						"Admiral", 
						"Sith Lord", 
						"Grand Moff"
					};
	    }
    }
}
