using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
	public class PerLengthPhaseImpedance : PerLengthImpedance
	{
		private int conductorCount;
		private List<long> phaseImpedanceDatas;
		public PerLengthPhaseImpedance(long globalId) : base (globalId)
		{

		}

		public int ConductorCount { get => conductorCount; set => conductorCount = value; }
		public List<long> PhaseImpedanceData { get => phaseImpedanceDatas; set => phaseImpedanceDatas = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				PerLengthPhaseImpedance p = (PerLengthPhaseImpedance)obj;
				return (p.conductorCount == this.conductorCount && CompareHelper.CompareLists(p.phaseImpedanceDatas, this.phaseImpedanceDatas));
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
				case ModelCode.PERLNGTPHSIMPD_CONDCOUNT:
					return true;
				case ModelCode.PERLNGTPHSIMPD_PHSIMPDDATAS:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.PERLNGTPHSIMPD_PHSIMPDDATAS:
					prop.SetValue(phaseImpedanceDatas);
					break;
				case ModelCode.PERLNGTPHSIMPD_CONDCOUNT:
					prop.SetValue(conductorCount);
					break;
				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.PERLNGTPHSIMPD_CONDCOUNT:
					conductorCount = property.AsInt();
					break;
				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return phaseImpedanceDatas.Count != 0 || base.IsReferenced;
			}
		}


		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (phaseImpedanceDatas != null && phaseImpedanceDatas.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.PERLNGTPHSIMPD_PHSIMPDDATAS] = phaseImpedanceDatas.GetRange(0, phaseImpedanceDatas.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.PHASEIMPDTA_PHSIMPDNC:
					phaseImpedanceDatas.Add(globalId);
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
				case ModelCode.PHASEIMPDTA_PHSIMPDNC:

					if (phaseImpedanceDatas.Contains(globalId))
					{
						phaseImpedanceDatas.Remove(globalId);
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
