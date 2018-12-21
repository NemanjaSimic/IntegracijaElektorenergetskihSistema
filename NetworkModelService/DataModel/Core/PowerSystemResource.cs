﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using FTN.Common;



namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class PowerSystemResource : IdentifiedObject
	{	
		
		public PowerSystemResource(long globalId)
			: base(globalId)
		{
		}	

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}		

		#region IAccess implementation
	
		#endregion IAccess implementation

		#region IReference implementation
		
		#endregion IReference implementation		
	}
}
