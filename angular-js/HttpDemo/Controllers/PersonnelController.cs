using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using AngularDemo.Models;

namespace AngularDemo.Controllers
{
    public class PersonnelController : ApiController
    {
	    public IEnumerable<Personnel>  Get()
	    {
		    return Global.PersonnelRepository.Personnel.OrderBy(p=>p.Id);
	    }

		public Personnel Get(int id)
		{
			return Global.PersonnelRepository.Personnel.FirstOrDefault(p => p.Id == id);
		}

		public Personnel Post(Personnel personnel)
		{
			var personnelList = Global.PersonnelRepository.Personnel;
			var existingPersonnel = personnelList.FirstOrDefault(p => p.Id == personnel.Id);
			personnelList.Remove(existingPersonnel);
			personnelList.Add(personnel);

			return personnel;
		}

    }
}
