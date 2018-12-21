using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
	public class PerLengthImpedance : Core.IdentifiedObject
	{
		private List<long> acLineSegments;
		public PerLengthImpedance(long globalId) : base(globalId)
		{

		}

		public List<long> AcLineSegments { get => acLineSegments; set => acLineSegments = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				PerLengthImpedance p = (PerLengthImpedance)obj;
				return (CompareHelper.CompareLists(p.acLineSegments,this.acLineSegments));
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

		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.PERLENIMP_ACLINSGMNTS:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.PERLENIMP_ACLINSGMNTS:
					prop.SetValue(acLineSegments);
					break;

				default:
					base.GetProperty(prop);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return acLineSegments.Count != 0 || base.IsReferenced;
			}
		}


		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (acLineSegments != null && acLineSegments.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.PERLENIMP_ACLINSGMNTS] = acLineSegments.GetRange(0, acLineSegments.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.ACLINESGMNT_PERLGTHIMPDNC:
					acLineSegments.Add(globalId);
					break;

				default:
					base.AddReference(referenceId, globalId);
					break;
			}
		}

		public override void RemoveReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.ACLINESGMNT:

					if (acLineSegments.Contains(globalId))
					{
						acLineSegments.Remove(globalId);
					}
					else
					{
						CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
					}

					break;

				default:
					base.RemoveReference(referenceId, globalId);
					break;
			}
		}


		#endregion IReference implementation
	}
}
