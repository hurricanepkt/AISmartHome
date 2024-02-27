namespace AISmartHome.Tests;

[TestFixture]
public class MessagePassingTests
{   
    IDictionary<string,string> vesselFields = new Dictionary<string,string>();
    [SetUp]
    public void Setup()
    {
        vesselFields =  typeof(Vessel).GetProperties().ToDictionary(x => x.Name, y => y.PropertyType.ToString());
    }

    [Test]
    public void BulkTest()
    {
        compare<AisParser.Messages.AddressedSafetyRelatedMessage>();
        compare<AisParser.Messages.AidToNavigationReportMessage>();
        compare<AisParser.Messages.BaseStationReportMessage>();
        compare<AisParser.Messages.BinaryAcknowledgeMessage>();
        compare<AisParser.Messages.BinaryBroadcastMessage>();
        compare<AisParser.Messages.DataLinkManagementMessage>();
        compare<AisParser.Messages.ExtendedClassBCsPositionReportMessage>();
        compare<AisParser.Messages.InterrogationMessage>();
        compare<AisParser.Messages.PositionReportClassAAssignedScheduleMessage>();
        compare<AisParser.Messages.PositionReportClassAMessage>();
        compare<AisParser.Messages.PositionReportClassAMessageBase>();
        compare<AisParser.Messages.PositionReportClassAResponseToInterrogationMessage>();
        compare<AisParser.Messages.PositionReportForLongRangeApplicationsMessage>();
        compare<AisParser.Messages.SafetyRelatedAcknowledgementMessage>();
        compare<AisParser.Messages.StandardClassBCsPositionReportMessage>();
        compare<AisParser.Messages.StandardSarAircraftPositionReportMessage>();
        compare<AisParser.Messages.StaticAndVoyageRelatedDataMessage>();
        compare<AisParser.Messages.StaticDataReportMessage>();
        compare<AisParser.Messages.StaticDataReportPartAMessage>();
        compare<AisParser.Messages.StaticDataReportPartBMessage>();
        compare<AisParser.Messages.UtcAndDateInquiryMessage>();
        compare<AisParser.Messages.UtcAndDateResponseMessage>();

    }

    public void compare<T>()
    {        
        Dictionary<string,string> bFields = typeof(T).GetProperties().ToDictionary(x => x.Name, y => y.PropertyType.ToString());
        foreach(var field in bFields.OrderBy(f=> f.Key)){
            Assert.That(vesselFields, Contains.Key(field.Key), $"Need Key {field.Key} with type {field.Value}");
        }
    }
}