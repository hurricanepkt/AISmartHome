using System.Text.Json;
using AisParser.Messages;
using Database;

// public interface IMessageStrategy {
//     bool CanDo(Type type);
//     Task Handle(object blah);
// }

// public abstract class BaseMessageStrategy : IMessageStrategy {
//     private FileContext _ctx;

//     public BaseMessageStrategy(FileContext ctx)
//     {
//         _ctx = ctx;
//     }
//     public virtual bool CanDo(Type type) { throw new NotImplementedException(); }
//     public virtual Task Handle(object blah) { throw new NotImplementedException(); }
//     public async Task<Vessel> GetOrCreate(uint mmsi) {
//         if (_ctx.Vessels.Any(f => f.Mmsi == mmsi)) {
//             return _ctx.Vessels.Single(f=> f.Mmsi == mmsi);
//         }
//         var ret = new Vessel(){ 
//             Mmsi = mmsi
//         };
//         await _ctx.Vessels.AddAsync(ret);
//         await _ctx.SaveChangesAsync();
//         return ret;
//     }
// }

// public class StaticDataReportPartBStrategy : BaseMessageStrategy 
// {
//     public StaticDataReportPartBStrategy(FileContext ctx) : base(ctx){ }

//     public override bool CanDo(Type type) {
//         return type == typeof(StaticDataReportPartBMessage);
//     }
//     public override async Task Handle(object blah) {
//         var msg = (StaticDataReportPartBMessage)blah;
//         var vessel = await GetOrCreate(msg.Mmsi);
//         StuffComparison.Compare(vessel, msg);
//         Console.WriteLine(JsonSerializer.Serialize(msg));
//     }
// }

// public class StandardClassBCsPositionReportStrategy : BaseMessageStrategy 
// {
//     public StandardClassBCsPositionReportStrategy(FileContext ctx) : base(ctx){ }
//     public override bool CanDo(Type type) {
//         return type == typeof(StandardClassBCsPositionReportMessage);
//     }
//     public override async Task Handle(object blah) {
//         var msg = (StandardClassBCsPositionReportMessage)blah;
//         var vessel = await GetOrCreate(msg.Mmsi);
//         StuffComparison.Compare(vessel, msg);
//         Console.WriteLine(JsonSerializer.Serialize(msg));
//     }
// }

// public class PositionReportClassAResponseToInterrogationStrategy : BaseMessageStrategy 
// {
//     public PositionReportClassAResponseToInterrogationStrategy(FileContext ctx) : base(ctx){ }
//     public override bool CanDo(Type type) {
//         return type == typeof(PositionReportClassAResponseToInterrogationMessage);
//     }
//     public override async Task Handle(object blah) {
//         var msg = (PositionReportClassAResponseToInterrogationMessage)blah;
//         var vessel = await GetOrCreate(msg.Mmsi);
//         StuffComparison.Compare(vessel, msg);
//         Console.WriteLine(JsonSerializer.Serialize(msg));
//     }
// }

// public class PositionReportClassAStrategy : BaseMessageStrategy 
// {
//     public PositionReportClassAStrategy(FileContext ctx) : base(ctx){ }
//     public override bool CanDo(Type type) {
//         return type == typeof(PositionReportClassAMessage);
//     }
//     public override async Task Handle(object blah) {
//         var msg = (PositionReportClassAMessage)blah;
//         var vessel = await GetOrCreate(msg.Mmsi);
//         StuffComparison.Compare(vessel, msg);
//         Console.WriteLine(JsonSerializer.Serialize(msg));
//     }
// }


// public class StaticDataReportPartAStrategy : BaseMessageStrategy 
// {
//     public StaticDataReportPartAStrategy(FileContext ctx) : base(ctx){ }
//     public override bool CanDo(Type type) {
//         return type == typeof(StaticDataReportPartAMessage);
//     }
//     public override async Task Handle(object blah) {
//         var msg = (StaticDataReportPartAMessage)blah;
//         var vessel = await GetOrCreate(msg.Mmsi);
//         StuffComparison.Compare(vessel, msg);
//         Console.WriteLine(JsonSerializer.Serialize(msg));
//     }
// }

// public class BinaryBroadcastStrategy : BaseMessageStrategy 
// {
//     public BinaryBroadcastStrategy(FileContext ctx) : base(ctx){ }
//     public override bool CanDo(Type type) {
//         return type == typeof(BinaryBroadcastMessage);
//     }
//     public override async Task Handle(object blah) {
//         var msg = (BinaryBroadcastMessage)blah;
//         var vessel = await GetOrCreate(msg.Mmsi);
//         StuffComparison.Compare(vessel, msg);
//         Console.WriteLine(JsonSerializer.Serialize(msg));
//     }
// }

public static class MessageStrategySetup {
    public static void Setup(IServiceCollection services) {
        // services.AddTransient<IMessageStrategy, AddressedSafetyRelatedStrategy>();
        // services.AddTransient<IMessageStrategy, AidToNavigationReportStrategy>();
        // services.AddTransient<IMessageStrategy, BaseStationReportStrategy>();
        // services.AddTransient<IMessageStrategy, BinaryAcknowledgeStrategy>();
        // services.AddTransient<IMessageStrategy, BinaryBroadcastStrategy>();
        // services.AddTransient<IMessageStrategy, DataLinkManagementStrategy>();
        // services.AddTransient<IMessageStrategy, ExtendedClassBCsPositionReportStrategy>();
        // services.AddTransient<IMessageStrategy, InterrogationStrategy>();
        // services.AddTransient<IMessageStrategy, PositionReportClassAAssignedScheduleStrategy>();
        // services.AddTransient<IMessageStrategy, PositionReportClassAStrategy>();
        // services.AddTransient<IMessageStrategy, PositionReportClassAMessageBase>();
        // services.AddTransient<IMessageStrategy, PositionReportClassAResponseToInterrogationStrategy>();
        // services.AddTransient<IMessageStrategy, PositionReportForLongRangeApplicationsStrategy>();
        // services.AddTransient<IMessageStrategy, SafetyRelatedAcknowledgementStrategy>();
        // services.AddTransient<IMessageStrategy, StandardClassBCsPositionReportStrategy>();
        // services.AddTransient<IMessageStrategy, StandardSarAircraftPositionReportStrategy>();
        // services.AddTransient<IMessageStrategy, StaticAndVoyageRelatedDataStrategy>();
        // services.AddTransient<IMessageStrategy, StaticDataReportStrategy>();
        // services.AddTransient<IMessageStrategy, StaticDataReportPartAStrategy>();
        // services.AddTransient<IMessageStrategy, StaticDataReportPartBStrategy>();
        // services.AddTransient<IMessageStrategy, UtcAndDateInquiryStrategy>();
        // services.AddTransient<IMessageStrategy, UtcAndDateResponseStrategy>();
    }
}


