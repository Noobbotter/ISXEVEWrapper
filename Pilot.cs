using System;
using System.Globalization;

using EVE.ISXEVE.Extensions;
using LavishScriptAPI;

namespace EVE.ISXEVE
{
	/// <summary>
	/// This is the Local TLO -- it is named LocalPilots to avoid reserved word conflict.
	/// </summary>
	public class Pilot : Being
	{
		#region Constructors
		/// <summary>
		/// LocalPilots copy constructor.
		/// </summary>
		/// <param name="Obj"></param>
		public Pilot(LavishScriptObject Obj)
			: base(Obj)
		{
		}

		/// <summary>
		/// Get a Local pilot by ID.
		/// </summary>
		public Pilot(Int64 CharID)
			: base(LavishScript.Objects.GetObject("Local", CharID.ToString(CultureInfo.CurrentCulture)))
		{
		}

		/// <summary>
		/// Get a Local pilot by name.
		/// </summary>
		public Pilot(string CharName)
			: base(LavishScript.Objects.GetObject("Local", CharName))
		{
		}
		#endregion

		#region Members

		private bool? _IsLimitedEngagement;
		/// <summary>
		/// Returns the IsIsLimitedEngagement flagg member of a Pilot object.
		/// </summary>
		public bool IsLimitedEngagement
		{
		    get
		    {
			if (_IsLimitedEngagement == null)
			    _IsLimitedEngagement = this.GetBool("IsLimitedEngagement");
			return _IsLimitedEngagement.Value;
		    }
		}

		private bool? _IsCriminal;
		/// <summary>
		/// Returns the Criminal  flag member of a Pilot object.
		/// </summary>
		public bool IsCriminal
		{
		    get
		    {
			if (_IsCriminal == null)
			    _IsCriminal = this.GetBool("IsCriminal");
			return _IsCriminal.Value;
		    }
		}

		private bool? _IsSuspect;
		/// <summary>
		/// Returns the Suspect  flag member of a Pilot object.
		/// </summary>
		public bool IsSuspect
		{
		    get
		    {
			if (_IsSuspect == null)
			    _IsSuspect = this.GetBool("IsSuspect");
			return _IsSuspect.Value;
		    }
		}

		private string _type;
		/// <summary>
		/// Wrapper for the Type member of a localpilots type.
		/// </summary>
		public string Type
		{
			get { return _type ?? (_type = this.GetString("Type")); }
		}

		private int? _typeId;
		/// <summary>
		/// This is, for the most part, your 'race' subtype)
		/// </summary>
		public int TypeID
		{
			get
			{
				if (_typeId == null)
					_typeId = this.GetInt("TypeID");
				return _typeId.Value;
			}
		}

		private Corporation _corp;
		public Corporation Corp
		{
			get
			{
				return _corp ?? (_corp = new Corporation(GetMember("Corp")));
			}
		}

		private string _alliance;
		/// <summary>
		/// Wrapper for the Alliance member of a localpilots type.
		/// </summary>
		public string Alliance
		{
			get { return _alliance ?? (_alliance = this.GetString("Alliance")); }
		}

		private int? _allianceId;
		/// <summary>
		/// Wrapper for the AllianceID member of a localpilots type.
		/// </summary>
		public int AllianceID
		{
			get
			{
				if (_allianceId == null)
					_allianceId = this.GetInt("AllianceID");
				return _allianceId.Value;
			}
		}

		private int? _WarFactionID;
		/// <summary>
		/// This is, FactionWar Country ID (-1 - NoFW, 500002 -Minmatarian, 500003 - Amarrian)
		/// </summary>
		public int WarFactionID
		{
		    get
		    {
			if (_WarFactionID == null)
			    _WarFactionID = this.GetInt("WarFactionID");
			return _WarFactionID.Value;
		    }
		}

		private string _allianceTicker;
		/// <summary>
		/// Wrapper for the AllianceTicker member of a localpilots type.
		/// </summary>
		public string AllianceTicker
		{
			get { return _allianceTicker ?? (_allianceTicker = this.GetString("AllianceTicker")); }
		}

		private Entity _toEntity;
		/// <summary>
		/// If they're within range of your overhead.
		/// </summary>
		public Entity ToEntity
		{
			get { return _toEntity ?? (_toEntity = new Entity(GetMember("ToEntity"))); }
		}

		private FleetMember _toFleetMemeber;
		/// <summary>
		/// Returns invalid if this pilot is not in your fleet.
		/// Use LavishScriptObject.IsNullOrInvalid to check.
		/// </summary>
		public FleetMember ToFleetMember
		{
			get { return _toFleetMemeber ?? (_toFleetMemeber = new FleetMember(GetMember("ToFleetMember"))); }
		}

		private Standing _standing;
		/// <summary>
		/// The pilots standing towards you.
		/// </summary>
		public Standing Standing
		{
			get
			{
				return _standing ?? (_standing = new Standing(GetMember("Standing")));
			}
		}

		/// <summary>
		/// The pilots standing towards any other CharID, CorporationID, or AllianceID
		/// </summary>
		public double StandingTo(int ID)
		{
			return this.GetDouble("StandingTo", ID.ToString(CultureInfo.CurrentCulture));
		}
		#endregion

		#region Methods
		/// <summary>
		/// EXAMPLE: new LocalPilots("Amadeus").SetStanding(5.0,"Amadeus rocks");
		/// </summary>
		public bool SetStanding(int Standing, string Reason)
		{
			Tracing.SendCallback("Pilot.SetStanding", Standing, Reason);
			return ExecuteMethod("SetStanding", Standing.ToString(CultureInfo.CurrentCulture), Reason);
		}

		#endregion
	}
}
