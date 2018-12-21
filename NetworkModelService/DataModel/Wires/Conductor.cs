using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
	public class Conductor : Core.ConductingEquipment
	{
		private float length;
		public Conductor(long globalId) : base(globalId)
		{

		}

		public float Length { get => length; set => length = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Conductor c = (Conductor)obj;
				return (c.length == this.length);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{
				case ModelCode.CONDUCTOR_LENGTH:
					return true;

				default:
					return base.HasProperty(t);

			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.CONDUCTOR_LENGTH:
					property.SetValue(length);
					break;

				default:
					base.GetProperty(property);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.CONDUCTOR_LENGTH:
					length = property.AsFloat();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation
	}
}
