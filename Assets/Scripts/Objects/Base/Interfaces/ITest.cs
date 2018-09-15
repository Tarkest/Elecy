
public interface ITest<O> where O : BaseObject
{
    O mObject { get; }
    O Dummy { get; }
}

