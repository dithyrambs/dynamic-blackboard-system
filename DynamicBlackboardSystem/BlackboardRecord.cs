namespace DynamicBlackboardSystem
{
    public enum BlackboardRecordType { TargetLastSeenTime, EnemyStatus }

    public struct BlackboardRecord
    {
        public BlackboardRecordType RecordType;
        public int TargetID;
        public int SubjectID;
        public float Value;

        public BlackboardRecord(BlackboardRecordType recordType, int targetID, int subjectID, float value)
        {
            RecordType = recordType;
            TargetID = targetID;
            SubjectID = subjectID;
            Value = value;
        }
    }
}