﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Bgc.Models
{
	public class ComradeRequest
	{
		[Key]
		public int      Id         {get; set;}
		public int      SenderId   {get; set;}
		public int      ReceiverId {get; set;}
		public DateTime Since      {get; set;}
	}
}
