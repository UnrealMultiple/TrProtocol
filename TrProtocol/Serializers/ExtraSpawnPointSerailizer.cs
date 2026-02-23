namespace TrProtocol.Models;

[Serializer(typeof(ExtraSpawnPointDataSerializer))]
public partial struct ExtraSpawnPointData
{
    private class ExtraSpawnPointDataSerializer : FieldSerializer<ExtraSpawnPointData>
    {
        protected override ExtraSpawnPointData ReadOverride(BinaryReader br)
        {
            var count = br.ReadByte();
            var data = new ExtraSpawnPointData()
            {
                Count = count,
                SpawnPoints = new ShortPosition[count]
            };
            for (int i = 0; i < count; i++)
            {
                data.SpawnPoints[i] = new ShortPosition(br.ReadInt16(), br.ReadInt16());
            }
            return data;
        }
        protected override void WriteOverride(BinaryWriter bw, ExtraSpawnPointData t)
        {
            bw.Write(t.Count);
            bw.Write((byte)t.SpawnPoints.Length);
            foreach (ShortPosition pos in t.SpawnPoints)
            {
                bw.Write(pos.X);
                bw.Write(pos.Y);
            }
        }
    }
}
