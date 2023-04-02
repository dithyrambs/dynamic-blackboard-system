using System.Linq;
using System.Collections.Generic;

namespace DynamicBlackboardSystem
{
    public class DynamicBlackboard
    {
        private List<BlackboardRecord> records = new List<BlackboardRecord>();

        public void AddRecord(BlackboardRecordType eType, int subjectID, int targetID = 0, float data = 0.0f)
        {
            var newRecord = new BlackboardRecord(eType, subjectID, targetID, data);
            records.Add(newRecord);
        }

        public void RemoveRecord(BlackboardRecordType eType, int subjectID)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].RecordType == eType && records[i].SubjectID == subjectID)
                {
                    indices.Add(i);
                }
            }

            foreach (int index in indices)
            {
                records.RemoveAt(index);
            }
        }

        public void RemoveRecords(BlackboardRecordType eType)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].RecordType == eType )
                {
                    indices.Add(i);
                }
            }

            foreach (int index in indices)
            {
                records.RemoveAt(index);
            }
        }

        public int CountRecords(BlackboardRecordType eType)
        {
            var newArray = records.Where(a => a.RecordType == eType).ToArray();
            return newArray.Length;
        }

        public int CountRecords(BlackboardRecordType eType, int subjectID )
        {
            var newArray = records.Where(a => a.RecordType == eType).Where(a => a.SubjectID == subjectID).ToArray();
            return newArray.Length;
        }

        public bool TryGetRecordData(BlackboardRecordType eType, out float[] values)
        {
            values = records.Where(a => a.RecordType == eType).Select(c => c.Value).ToArray();
            var result = values.Length > 0;
            return result;
        }

        public bool TryGetRecordData(BlackboardRecordType eType, int subjectID, out float[] values)
        {
            values = records.Where(a => a.RecordType == eType).Where(b => b.SubjectID == subjectID).Select(c => c.Value).ToArray();
            var result = values.Length > 0;
            return result;
        }
    }
}

