using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularDemo.Models
{
	public class PersonnelRepository
	{
		public List<Personnel> Personnel { get; set; }

		public List<string> Titles { get; set; }

		public PersonnelRepository()
		{
			Titles = new List<string>
			{
				"Captain", 
				"Admiral", 
				"Sith Lord", 
				"Grand Moff"
			};

			Personnel = new List<Personnel>
			{
				new Personnel
				{
					FirstName = "Lorth",
					LastName = "Needa",
					Title = "Captain",
					Image = "/images/needa.jpg",
					Active = true,
					Id = 5,
					DetailsUrl = "http://www.starwars.com/databank/captain-needa"
				},
				new Personnel
				{
					FirstName = "Darth",
					LastName = "Vader",
					Title = "Sith Lord",
					Image = "/images/vader.jpg",
					Active = true,
					Id = 2,
					DetailsUrl = "http://www.starwars.com/databank/darth-vader"
				},
				new Personnel
				{
					FirstName = "Kendal",
					LastName = "Ozzel",
					Title = "Admiral",
					Image = "/images/ozzel.jpg",
					Active = true,
					Id = 3,
					DetailsUrl = "http://www.starwars.com/databank/admiral-ozzel"
				},
				new Personnel
				{
					FirstName = "Firmus",
					LastName = "Piett",
					Title = "Captain",
					Image = "/images/piett.jpg",
					Active = true,
					Id = 4,
					DetailsUrl = "http://www.starwars.com/databank/admiral-piett"
				},
				new Personnel
				{
					FirstName = "Wilhuff",
					LastName = "Tarkin",
					Title = "Grand Moff",
					Image = "/images/tarkin.jpg",
					Active = false,
					Id = 1,
					DetailsUrl = "http://www.starwars.com/databank/tarkin"
				}
			};
		}
	}
}