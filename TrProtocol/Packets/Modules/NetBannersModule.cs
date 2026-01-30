
namespace TrProtocol.Packets.Modules;

public interface IBannersModule
{
    public BannersMessageType BannersType { get; }

    public void Serialize(BinaryWriter bw);

    public IBannersModule Deserialize(BinaryReader br);
}

[Serializer(typeof(BannersModuleSerializer))]
public class BannersAdpter
{
    public BannersMessageType BannersType { get; set; }

    public IBannersModule BannersData { get; set; }

    private class BannersModuleSerializer : FieldSerializer<BannersAdpter>
    {
        protected override BannersAdpter ReadOverride(BinaryReader br)
        {
            var data = new BannersAdpter()
            {
                BannersType = (BannersMessageType)br.ReadByte(),
            };
            data.BannersData = data.BannersType switch
            {
                BannersMessageType.FullState => new FullStateModule().Deserialize(br),
                BannersMessageType.KillCountUpdate => new KillCountUpdateModule().Deserialize(br),
                BannersMessageType.ClaimCountUpdate => new ClaimCountUpdateModule().Deserialize(br),
                BannersMessageType.ClaimRequest => new ClaimRequestModule().Deserialize(br),
                BannersMessageType.ClaimResponse => new ClaimResponseModule().Deserialize(br),
                _ => throw new NotImplementedException(),
            };
            return data;
        }

        protected override void WriteOverride(BinaryWriter bw, BannersAdpter t)
        {
            bw.Write((byte)t.BannersType);
            t.BannersData.Serialize(bw);
        }
    }
}

public struct FullStateModule : IBannersModule
{
    public readonly BannersMessageType BannersType => BannersMessageType.FullState;
    public short KillCount { get; set; }
    public int[] Kills { get; set; }
    public short ClaimableBannerCount { get; set; }
    public ushort[] ClaimableBanners { get; set; }

    public IBannersModule Deserialize(BinaryReader br)
    {
        var module = new FullStateModule
        {
            KillCount = br.ReadInt16()
        };
        module.Kills = new int[module.KillCount];
        for (int i = 0; i < module.KillCount; i++)
        {
            module.Kills[i] = br.ReadInt32();
        }
        module.ClaimableBannerCount = br.ReadInt16();
        module.ClaimableBanners = new ushort[module.ClaimableBannerCount];
        for (int i = 0; i < module.ClaimableBannerCount; i++)
        {
            module.ClaimableBanners[i] = br.ReadUInt16();
        }
        return module;
    }

    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(KillCount);
        foreach (var kill in Kills)
        {
            bw.Write(kill);
        }
        bw.Write(ClaimableBannerCount);
        foreach (var banner in ClaimableBanners)
        {
            bw.Write(banner);
        }
    }
}

public struct KillCountUpdateModule : IBannersModule
{
    public short BannerId { get; set; }
    public int Kill { get; set; }

    public BannersMessageType BannersType => BannersMessageType.KillCountUpdate;

    public IBannersModule Deserialize(BinaryReader br)
    {
        return new KillCountUpdateModule
        {
            BannerId = br.ReadInt16(),
            Kill = br.ReadInt32()
        };
    }

    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(BannerId);
        bw.Write(Kill);
    }
}

public struct ClaimCountUpdateModule : IBannersModule
{
    public readonly BannersMessageType BannersType => BannersMessageType.ClaimCountUpdate;
    public short BannerId { get; set; }
    public ushort ClaimableBanner { get; set; }

    public IBannersModule Deserialize(BinaryReader br)
    {
        return new ClaimCountUpdateModule
        {
            BannerId = br.ReadInt16(),
            ClaimableBanner = br.ReadUInt16()
        };
    }

    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(BannerId);
        bw.Write(ClaimableBanner);
    }
}

public struct ClaimRequestModule : IBannersModule
{
    public BannersMessageType BannersType => BannersMessageType.ClaimRequest;
    public short BannerId { get; set; }
    public ushort Amount { get; set; }

    public IBannersModule Deserialize(BinaryReader br)
    {
        return new ClaimRequestModule
        {
            BannerId = br.ReadInt16(),
            Amount = br.ReadUInt16()
        };
    }

    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(BannerId);
        bw.Write(Amount);
    }
}

public struct ClaimResponseModule : IBannersModule
{
    public BannersMessageType BannersType => BannersMessageType.ClaimResponse;
    public short BannerId { get; set; }
    public ushort Amount { get; set; }
    public bool Granted { get; set; }

    public IBannersModule Deserialize(BinaryReader br)
    {
        return new ClaimResponseModule
        {
            BannerId = br.ReadInt16(),
            Amount = br.ReadUInt16(),
            Granted = br.ReadBoolean()
        };
    }

    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(BannerId);
        bw.Write(Amount);
        bw.Write(Granted);
    }
}


public class NetBannersModule : NetModulesPacket
{
    public override NetModuleType ModuleType => NetModuleType.BannerSystemNetBannersModule;

    public override MessageID Type => MessageID.NetModules;

    public BannersAdpter BannersData { get; set; }
}
