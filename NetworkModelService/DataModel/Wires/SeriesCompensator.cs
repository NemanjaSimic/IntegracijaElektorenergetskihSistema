using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
	public class SeriesCompensator : Core.ConductingEquipment
	{
		private float r;
		private float r0;
		private float x;
		private float x0;
		public SeriesCompensator(long globalId) : base(globalId)
		{

		}

		public float R { get => r; set => r = value; }
		public float R0 { get => r0; set => r0 = value; }
		public float X { get => x; set => x = value; }
		public float X0 { get => x0; set => x0 = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				SeriesCompensator s = (SeriesCompensator)obj;
				return (s.r == this.r && s.r0 == this.r0 && s.x == this.x && s.x0 == this.x0);
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
				case ModelCode.SERIESCMPNSTR_R:
					return true;
				case ModelCode.SERIESCMPNSTR_R0:
					return true;
				case ModelCode.SERIESCMPNSTR_X:
					return true;
				case ModelCode.SERIESCMPNSTR_X0:
					return true;
				default:
					return base.HasProperty(t);

			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.SERIESCMPNSTR_R:
					property.SetValue(r);
					break;
				case ModelCode.SERIESCMPNSTR_R0:
					property.SetValue(r0);
					break;
				case ModelCode.SERIESCMPNSTR_X:
					property.SetValue(x);
					break;
				case ModelCode.SERIESCMPNSTR_X0:
					property.SetValue(x0);
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
				case ModelCode.SERIESCMPNSTR_R:
					r = property.AsFloat();
					break;
				case ModelCode.SERIESCMPNSTR_R0:
					r0 = property.AsFloat();
					break;
				case ModelCode.SERIESCMPNSTR_X:
					x = property.AsFloat();
					break;
				case ModelCode.SERIESCMPNSTR_X0:
					x0 = property.AsFloat();
					break;
				default:
					base.SetProperty(property);
					break;
			}
		}
		#endregion IAccess implementation
	}
}
