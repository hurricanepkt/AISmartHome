using System.ComponentModel.DataAnnotations;
using AisParser;
using Geolocation;
using Infrastructure;

public class VesselDto {
    public static VesselDto Create(Vessel source)
    {
        var retVal = new VesselDto();
        Copy(retVal, source);
        return retVal;
    }

    private static void Copy(VesselDto vesselDto, Vessel vessel)
    {   
        var dtoProperties = vesselDto.GetType().GetProperties();
        var vesselProperties = (typeof(Vessel)).GetProperties();
        foreach (var dtoProperty in dtoProperties)
        {   
            try {                
                var pt = vesselProperties.First(f=> f.Name == dtoProperty.Name);
                dtoProperty.SetValue(vesselDto, pt.GetValue(vessel));
            }
            catch(Exception ex)
            {
                throw new Exception($"Cant set property {dtoProperty.Name} with {dtoProperty.PropertyType}", ex);
            }
        }
    }
    
    [Key]
    public uint Mmsi {get; set;}
    public UInt32? Reserved {get; set;}
    public Double? SpeedOverGround {get; set;}
    public int? RateOfTurn {get; set;}
    public Double? Longitude {get;set;}
    public Double? Latitude {get;set;}
    public Double? CourseOverGround {get;set;}
    public UInt32? TrueHeading {get; set;}
    public UInt32? Timestamp {get; set;}
    public string? ShipName {get;set;}
    public string? Name {get; set;}
    public string? NameExtension {get; set;}
    public bool? IsCsUnit {get;set;}
    public bool? RetransmitFlag {get;set;}
    public string? Text {get;set;}  
    public UInt32? DimensionToBow {get; set;} 
    public UInt32? DimensionToStern {get; set;}  
    public UInt32? DimensionToPort {get; set;}   
    public UInt32? DimensionToStarboard {get; set;}    
    public bool? OffPosition {get;set;}
    public bool? VirtualAid {get;set;} 
    public string? Data {get;set;} 
    public bool? DataTerminalReady {get;set;}      
    public UInt32? Altitude {get;set;}
    public UInt32? AisVersion {get;set;}
    public string? CallSign {get;set;}
    public string? Destination {get;set;}
    public Double? Draught {get;set;}
    public GnssPositionStatus? GnssPositionStatus {get; set;}
    public PositionFixType?  PositionFixType {get; set;}
    public NavigationStatus? NavigationStatus {get; set;}
    public NavigationalAidType? NavigationalAidType {get;set;} 
    public AisMessageType? MessageType {get; set;}
    public AisMessageType? AisMessageType {get; set;}
    public PositionAccuracy? PositionAccuracy {get; set;}  
    public ManeuverIndicator?  ManeuverIndicator {get; set;}
    public ShipType? ShipType {get;set;}
    public Raim? Raim {get;set;}
    public DateTime LastUpdate { get; internal set; }

    // Calculated Vessel Properties
    public double Distance { get;set; }
    public int SecondsAgo {get; set;}
    public bool? IsCommercial { get; set; }

    /*
        public UInt32? SequenceNumber {get; set;} 
        public UInt32? DestinationMmsi {get; set;}  
        public UInt32? PartNumber {get; set;}
        public UInt32? RadioStatus {get; set;}
        public bool? HasDisplay {get;set;}
        public UInt32? Repeat {get; set;}
        public bool? HasDscCapability {get;set;}
        public bool? Band {get;set;}
        public bool? CanAcceptMessage22 {get; set;}
        public bool? Assigned {get; set;}
        public UInt32? Year {get; set;}   
        public UInt32? Month {get; set;}   
        public UInt32? Day {get; set;}   
        public UInt32? Hour {get; set;}   
        public UInt32? Minute {get; set;}   
        public UInt32? Second {get; set;}  
        public UInt32? DesignatedAreaCode {get;set;}
        public UInt32? FunctionalId {get;set;}
        public UInt32? EtaDay {get;set;}
        public UInt32? EtaHour {get;set;}
        public UInt32? EtaMinute {get;set;}
        public UInt32? EtaMonth {get;set;}
        public UInt32? ImoNumber {get;set;}
        public UInt32? UnitModelCode {get;set;} 
        public UInt32? SerialNumber {get;set;} 
        public string? VendorId {get;set;}    
        public UInt32? MothershipMmsi {get;set;}
        public UInt32? RegionalReserved {get; set;}
        public UInt32? Spare {get; set;}
        public UInt32? Spare1 {get; set;}
        public UInt32? Spare2 {get; set;}
        public UInt32? Offset1 {get;set;}
        public UInt32? Offset2 {get;set;}
        public UInt32? Offset3 {get;set;}
        public UInt32? Offset4 {get;set;}
        public UInt32? ReservedSlots1 {get;set;}
        public UInt32? ReservedSlots2 {get;set;}
        public UInt32? ReservedSlots3 {get;set;}
        public UInt32? ReservedSlots4 {get;set;}
        public UInt32? Timeout1 {get;set;}
        public UInt32? Timeout2 {get;set;}
        public UInt32? Timeout3 {get;set;}
        public UInt32? Timeout4 {get;set;}
        public UInt32? Increment1 {get;set;}
        public UInt32? Increment2 {get;set;}    
        public UInt32? Increment3 {get;set;}  
        public UInt32? Increment4 {get;set;} 
        public UInt32? SequenceNumber1 {get; set;}  
        public UInt32? SequenceNumber2 {get; set;}  
        public UInt32? SequenceNumber3 {get; set;}  
        public UInt32? SequenceNumber4 {get; set;}  
        public UInt32? FirstSlotOffset {get;set;}  
        public UInt32? SecondSlotOffset {get;set;} 
        public UInt32? SecondStationFirstSlotOffset {get;set;} 
        public UInt32? InterrogatedMmsi {get;set;} 
        public UInt32? SecondStationInterrogationMmsi {get;set;} 
        public AisMessageType? FirstMessageType {get; set;} 
        public AisMessageType? SecondMessageType {get; set;}
        public AisMessageType? SecondStationFirstMessageType {get; set;}   
        public UInt32? Mmsi1 {get;set;}
        public UInt32? Mmsi2 {get;set;}
        public UInt32? Mmsi3 {get;set;}
        public UInt32? Mmsi4 {get;set;}
    */
}



