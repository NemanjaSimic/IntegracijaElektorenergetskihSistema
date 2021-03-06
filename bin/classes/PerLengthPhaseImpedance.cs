//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTN {
    using System;
    using FTN;
    
    
    /// Impedance and admittance parameters per unit length for n-wire unbalanced lines, in matrix form.
    public class PerLengthPhaseImpedance : PerLengthImpedance {
        
        /// Number of phase, neutral, and other wires retained. Constrains the number of matrix elements and the phase codes that can be used with this matrix.
        private System.Int32? cim_conductorCount;
        
        private const bool isConductorCountMandatory = false;
        
        private const string _conductorCountPrefix = "cim";
        
        public virtual int ConductorCount {
            get {
                return this.cim_conductorCount.GetValueOrDefault();
            }
            set {
                this.cim_conductorCount = value;
            }
        }
        
        public virtual bool ConductorCountHasValue {
            get {
                return this.cim_conductorCount != null;
            }
        }
        
        public static bool IsConductorCountMandatory {
            get {
                return isConductorCountMandatory;
            }
        }
        
        public static string ConductorCountPrefix {
            get {
                return _conductorCountPrefix;
            }
        }
    }
}
