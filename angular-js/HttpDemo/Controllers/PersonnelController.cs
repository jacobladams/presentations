using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using HttpDemo.Models;

namespace HttpDemo.Controllers
{
    public class PersonnelController : ApiController
    {
	    public IEnumerable<Personnel>  Get()
	    {
		    return Global.PersonnelList.OrderBy(p=>p.Id);
	    }

		public Personnel Get(int id)
		{
			return Global.PersonnelList.FirstOrDefault(p => p.Id == id);
		}

		public Personnel Post(Personnel personnel)
		{
			//Global.PersonnelList[personnel.Id] = personnel;
			var existingPersonnel = Global.PersonnelList.FirstOrDefault(p => p.Id == personnel.Id);
			Global.PersonnelList.Remove(existingPersonnel);
			Global.PersonnelList.Add(personnel);

			return personnel;
		}

    }
}
