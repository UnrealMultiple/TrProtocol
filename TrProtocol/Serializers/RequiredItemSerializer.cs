namespace TrProtocol.Models;

[Serializer(typeof(RequiredItemDataSerializer))]
public partial struct RequiredItemData
{
    private class RequiredItemDataSerializer : FieldSerializer<RequiredItemData>
    {
        protected override RequiredItemData ReadOverride(BinaryReader br)
        {
            var data = new RequiredItemData()
            {
                ItemCount = Read7BitEncodedInt(br),
            };
            var items = new RequiredItemEntry[data.ItemCount];
            for (int i = 0; i < data.ItemCount; i++)
            {
                items[i] = new RequiredItemEntry()
                {
                    ItemIdOrRecipeGroup = br.ReadInt32(),
                    Stack = Read7BitEncodedInt(br),
                };
            }
            data.ChestCount = Read7BitEncodedInt(br);
            var chestSlots = new int[data.ChestCount];
            for (int i = 0; i < data.ChestCount; i++)
            {
                chestSlots[i] = Read7BitEncodedInt(br);
            }
            data.ItemEntries = items;
            data.ChestSlots = chestSlots;
            return data;
        }

        protected override void WriteOverride(BinaryWriter bw, RequiredItemData t)
        {
            Write7BitEncodedInt(bw, t.ItemCount);
            for (int i = 0; i < t.ItemCount; i++)
            {
                bw.Write(t.ItemEntries[i].ItemIdOrRecipeGroup);
                Write7BitEncodedInt(bw, t.ItemEntries[i].Stack);
            }
            Write7BitEncodedInt(bw, t.ChestCount);
            for (int i = 0; i < t.ChestCount; i++)
            {
                Write7BitEncodedInt(bw, t.ChestSlots[i]);
            }
        }

        private static void Write7BitEncodedInt(BinaryWriter writer, int value)
        {
            uint num;
            for (num = (uint)value; num > 127; num >>= 7)
            {
                writer.Write((byte)(num | 0xFFFFFF80u));
            }
            writer.Write((byte)num);
        }

        private static int Read7BitEncodedInt(BinaryReader reader)
        {
            uint num = 0u;
            byte b;
            for (int i = 0; i < 28; i += 7)
            {
                b = reader.ReadByte();
                num |= (uint)((b & 0x7F) << i);
                if ((uint)b <= 127u)
                {
                    return (int)num;
                }
            }
            b = reader.ReadByte();
            if (b > 15)
            {
                throw new FormatException("Bad 7bit encoded int");
            }
            return (int)num | (b << 28);
        }
    }
}
