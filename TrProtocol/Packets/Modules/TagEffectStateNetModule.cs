namespace TrProtocol.Packets.Modules;

public interface ITagEffectStateNetModule
{
    TagEffectType TagEffectType { get; }

    public void Serialize(BinaryWriter bw);

    public ITagEffectStateNetModule Deserialize(BinaryReader br);
}
public struct TagEffectFullStateModule : ITagEffectStateNetModule
{
    public short Type { get; set; }

    public int[] TimeLeftOnNPCs { get; set; }
    public int[] ProcTimeLeftOnNPCs { get; set; }
    public readonly TagEffectType TagEffectType => TagEffectType.FullState;

    public ITagEffectStateNetModule Deserialize(BinaryReader br)
    {
        var data = new TagEffectFullStateModule
        {
            Type = br.ReadInt16(),
            TimeLeftOnNPCs = new int[200]
        };
        ReadSparseNPCTimeArray(br, data.TimeLeftOnNPCs);
        if (br.BaseStream.Position < br.BaseStream.Length)
        {
            data.ProcTimeLeftOnNPCs = new int[200];
            ReadSparseNPCTimeArray(br, data.ProcTimeLeftOnNPCs);
        }
        return data;
    }

    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(Type);
        WriteSparseNPCTimeArray(bw, TimeLeftOnNPCs);
        WriteSparseNPCTimeArray(bw, ProcTimeLeftOnNPCs);
    }

    public static void ReadSparseNPCTimeArray(BinaryReader reader, int[] array)
    {
        while (true)
        {
            int num = reader.ReadByte();
            if (num < array.Length)
            {
                array[num] = reader.ReadInt32();
                continue;
            }
            break;
        }
    }

    public static void WriteSparseNPCTimeArray(BinaryWriter writer, int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int num = array[i];
            if (num != 0)
            {
                writer.Write((byte)i);
                writer.Write(num);
            }
        }
        writer.Write((byte)array.Length);
    }
}

public struct TagEffectChangeActiveEffectModule : ITagEffectStateNetModule
{
    public readonly TagEffectType TagEffectType => TagEffectType.ChangeActiveEffect;
    public short Type { get; set; }
    public ITagEffectStateNetModule Deserialize(BinaryReader br)
    {
        return new TagEffectChangeActiveEffectModule() { Type = br.ReadInt16() };
    }
    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(Type);
    }
}

public struct TagEffectApplyTagToNPCModule : ITagEffectStateNetModule
{
    public readonly TagEffectType TagEffectType => TagEffectType.ApplyTagToNPC;

    public byte NPCSlot { get; set; }

    public ITagEffectStateNetModule Deserialize(BinaryReader br)
    {
        return new TagEffectApplyTagToNPCModule() { NPCSlot = br.ReadByte() };
    }
    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(NPCSlot);
    }
}

public struct TagEffectEnableProcOnNPCModule : ITagEffectStateNetModule
{
    public byte NPCSlot { get; set; }
    public readonly TagEffectType TagEffectType => TagEffectType.EnableProcOnNPC;
    public ITagEffectStateNetModule Deserialize(BinaryReader br)
    {
        return new TagEffectEnableProcOnNPCModule() { NPCSlot = br.ReadByte() };
    }
    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(NPCSlot);
    }
}

public struct TagEffectClearProcOnNPCModule : ITagEffectStateNetModule
{
    public byte NPCSlot { get; set; }
    public readonly TagEffectType TagEffectType => TagEffectType.ClearProcOnNPC;
    public ITagEffectStateNetModule Deserialize(BinaryReader br)
    {
        return new TagEffectClearProcOnNPCModule() { NPCSlot = br.ReadByte() };
    }
    public readonly void Serialize(BinaryWriter bw)
    {
        bw.Write(NPCSlot);
    }
}

[Serializer(typeof(TagEffectModuleSerializer))]
public class TagEffectAdapter
{
    public TagEffectType TagEffectType { get; set; }
    public ITagEffectStateNetModule TagEffectData { get; set; }
    private class TagEffectModuleSerializer : FieldSerializer<TagEffectAdapter>
    {
        protected override TagEffectAdapter ReadOverride(BinaryReader br)
        {
            var data = new TagEffectAdapter()
            {
                TagEffectType = (TagEffectType)br.ReadByte(),
            };
            data.TagEffectData = data.TagEffectType switch
            {
                TagEffectType.FullState => new TagEffectFullStateModule().Deserialize(br),
                TagEffectType.ChangeActiveEffect => new TagEffectChangeActiveEffectModule().Deserialize(br),
                TagEffectType.ApplyTagToNPC => new TagEffectApplyTagToNPCModule().Deserialize(br),
                TagEffectType.EnableProcOnNPC => new TagEffectEnableProcOnNPCModule().Deserialize(br),
                TagEffectType.ClearProcOnNPC => new TagEffectClearProcOnNPCModule().Deserialize(br),
                _ => throw new NotImplementedException(),
            };
            return data;
        }
        protected override void WriteOverride(BinaryWriter bw, TagEffectAdapter t)
        {
            bw.Write((byte)t.TagEffectType);
            t.TagEffectData.Serialize(bw);
        }
}
}

public class TagEffectStateNetModule : NetModulesPacket, IPlayerSlot
{
    public override NetModuleType ModuleType => NetModuleType.TagEffectStateNetModule;

    public override MessageID Type => MessageID.NetModules;

    public byte PlayerSlot { get; set; }

    public TagEffectAdapter TagEffectData { get; set; }

}
