using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
	public class PhaseImpedanceData : Core.IdentifiedObject
	{
		private float b;
		private float r;
		private int sequenceNumber;
		private float x;
		private long phaseImedance;
		public PhaseImpedanceData(long globalId) : base(globalId)
		{

		}

		public float B { get => b; set => b = value; }
		public float R { get => r; set => r = value; }
		public int SequenceNumber { get => sequenceNumber; set => sequenceNumber = value; }
		public float X { get => x; set => x = value; }
		public long PhaseImedance { get => phaseImedance; set => phaseImedance = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				PhaseImpedanceData p = (PhaseImpedanceData)obj;
				return (p.b == this.b && p.r == this.r && p.sequenceNumber==this.sequenceNumber && p.x == this.x && p.phaseImedance==this.phaseImedance);
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
				case ModelCode.PHASEIMPDTA_B:
					return true;
				case ModelCode.PHASEIMPDTA_R:
					return true;
				case ModelCode.PHASEIMPDTA_SEQNUM:
					return true;
				case ModelCode.PHASEIMPDTA_X:
					return true;
				case ModelCode.PHASEIMPDTA_PHSIMPDNC:
					return true;
				default:
					return base.HasProperty(t);

			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.PHASEIMPDTA_B:
					property.SetValue(b);
					break;
				case ModelCode.PHASEIMPDTA_R:
					property.SetValue(r);
					break;
				case ModelCode.PHASEIMPDTA_SEQNUM:
					property.SetValue(sequenceNumber);
					break;
				case ModelCode.PHASEIMPDTA_X:
					property.SetValue(x);
					break;
				case ModelCode.PHASEIMPDTA_PHSIMPDNC:
					property.SetValue(phaseImedance);
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
				case ModelCode.PHASEIMPDTA_B:
					b = property.AsFloat();
					break;
				case ModelCode.PHASEIMPDTA_R:
					r = property.AsFloat();
					break;
				case ModelCode.PHASEIMPDTA_SEQNUM:
					sequenceNumber = property.AsInt();
					break;
				case ModelCode.PHASEIMPDTA_X:
					x = property.AsFloat();
					break;
				case ModelCode.PHASEIMPDTA_PHSIMPDNC:
					b = property.AsReference();
					break;
				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (phaseImedance != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.PHASEIMPDTA_PHSIMPDNC] = new List<long>();
				references[ModelCode.PHASEIMPDTA_PHSIMPDNC].Add(phaseImedance);
			}
			base.GetReferences(references, refType);
		}

		#endregion IReference implementation
	}
}
