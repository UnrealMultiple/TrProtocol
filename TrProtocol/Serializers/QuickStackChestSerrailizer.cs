namespace TrProtocol.Models;

[Serializer(typeof(QuickStackChestDataSerializer))]
public partial struct QuickStackChestData
{
    private class QuickStackChestDataSerializer : FieldSerializer<QuickStackChestData>
    {
        protected override QuickStackChestData ReadOverride(BinaryReader br)
        {
            var count = br.ReadInt32();
            var data = new QuickStackChestData()
            {
                ItemCount = count,
                SlotIds = new short[count]
            };
            for (int i = 0; i < data.SlotIds.Length; i++)
            {
                data.SlotIds[i] = br.ReadInt16();
            }
            data.ChestSlot = br.ReadInt16();
            return data;
        }
        protected override void WriteOverride(BinaryWriter bw, QuickStackChestData t)
        {
            bw.Write(t.ItemCount);
            bw.Write(t.SlotIds.Length);
            foreach (short slotId in t.SlotIds)
            {
                bw.Write(slotId);
            }
            bw.Write(t.ChestSlot);
        }
    }
}
