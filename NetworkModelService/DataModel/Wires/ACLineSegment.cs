using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
	public class ACLineSegment : Conductor
	{
		private float b0ch;
		private float bch;
		private float g0ch;
		private float gch;
		private float r;
		private float r0;
		private float x;
		private float x0;
		private long perLengthImpedance;

		public ACLineSegment(long globalId) : base(globalId)
		{

		}

		public float B0ch { get => b0ch; set => b0ch = value; }
		public float Bch { get => bch; set => bch = value; }
		public float G0ch { get => g0ch; set => g0ch = value; }
		public float Gch { get => gch; set => gch = value; }
		public float R { get => r; set => r = value; }
		public float R0 { get => r0; set => r0 = value; }
		public float X { get => x; set => x = value; }
		public float X0 { get => x0; set => x0 = value; }
		public long PerLengthImpedance { get => perLengthImpedance; set => perLengthImpedance = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				ACLineSegment a = (ACLineSegment)obj;
				return (a.b0ch == this.b0ch && a.bch == this.bch && a.g0ch == this.g0ch && a.gch == this.gch && a.r == this.r && a.r0 == this.r0 && a.x == this.x && a.x0 == this.x0 && a.perLengthImpedance == this.perLengthImpedance);
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
				case ModelCode.ACLINESGMNT_B0CH:
					return true;
				case ModelCode.ACLINESGMNT_BCH:
					return true;
				case ModelCode.ACLINESGMNT_G0CH:
					return true;
				case ModelCode.ACLINESGMNT_GCH:
					return true;
				case ModelCode.ACLINESGMNT_R:
					return true;
				case ModelCode.ACLINESGMNT_R0:
					return true;
				case ModelCode.ACLINESGMNT_X:
					return true;
				case ModelCode.ACLINESGMNT_X0:
					return true;
				case ModelCode.ACLINESGMNT_PERLGTHIMPDNC:
					return true;

				default:
					return base.HasProperty(t);

			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.ACLINESGMNT_B0CH:
					property.SetValue(b0ch);
					break;
				case ModelCode.ACLINESGMNT_BCH:
					property.SetValue(bch);
					break;
				case ModelCode.ACLINESGMNT_G0CH:
					property.SetValue(g0ch);
					break;
				case ModelCode.ACLINESGMNT_GCH:
					property.SetValue(gch);
					break;
				case ModelCode.ACLINESGMNT_R:
					property.SetValue(r);
					break;
				case ModelCode.ACLINESGMNT_R0:
					property.SetValue(r0);
					break;
				case ModelCode.ACLINESGMNT_X:
					property.SetValue(x);
					break;
				case ModelCode.ACLINESGMNT_X0:
					property.SetValue(x0);
					break;
				case ModelCode.ACLINESGMNT_PERLGTHIMPDNC:
					property.SetValue(perLengthImpedance);
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
				case ModelCode.ACLINESGMNT_B0CH:
					b0ch = property.AsFloat();
					break;
				case ModelCode.ACLINESGMNT_BCH:
					bch = property.AsFloat();
					break;
				case ModelCode.ACLINESGMNT_G0CH:
					g0ch = property.AsFloat();
					break;
				case ModelCode.ACLINESGMNT_GCH:
					gch = property.AsFloat();
					break;
				case ModelCode.ACLINESGMNT_R:
					r = property.AsFloat();
					break;
				case ModelCode.ACLINESGMNT_R0:
					r0 = property.AsFloat();
					break;
				case ModelCode.ACLINESGMNT_X:
					x = property.AsFloat();
					break;
				case ModelCode.ACLINESGMNT_X0:
					x0 = property.AsFloat();
					break;
				case ModelCode.ACLINESGMNT_PERLGTHIMPDNC:
					perLengthImpedance = property.AsReference();
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
			if (perLengthImpedance != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.ACLINESGMNT_PERLGTHIMPDNC] = new List<long>();
				references[ModelCode.ACLINESGMNT_PERLGTHIMPDNC].Add(perLengthImpedance);
			}
			base.GetReferences(references, refType);
		}

		#endregion IReference implementation
	}
}
