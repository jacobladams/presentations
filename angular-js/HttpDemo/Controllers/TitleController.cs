using System.Collections.Generic;
using System.Web.Http;

using AngularDemo;

namespace HttpDemo.Controllers
{
    public class TitleController : ApiController
    {
	    public IEnumerable<string> Get()
	    {
		   return Global.PersonnelRepository.Titles;
	    }
    }
}
